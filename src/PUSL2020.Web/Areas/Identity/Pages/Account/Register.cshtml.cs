// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using PUSL2020.Application.DataAnnotations;
using PUSL2020.Application.Identity.Models;
using PUSL2020.Application.Services;
using PUSL2020.Domain.Entities;
using PUSL2020.Domain.ValueObjects;

namespace PUSL2020.Web.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ReporterUser> _signInManager;
        private readonly UserManager<ReporterUser> _userManager;
        private readonly IUserStore<ReporterUser> _userStore;
        private readonly IUserEmailStore<ReporterUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;

        public RegisterModel(
            UserManager<ReporterUser> userManager,
            IUserStore<ReporterUser> userStore,
            SignInManager<ReporterUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IMapper mapper)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _mapper = mapper;
        }

        
        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }
        
        #region Wizard
        public class PersonalDetailsModel
        {
            [Display(Name = "First Name")]
            public string FirstName { get; set; }
            
            [Display(Name = "Middle Name")]
            public string MiddleName { get; set; }
            
            [Display(Name = "Last Name")]
            public string LastName { get; set; }
            
            [Display(Name = "Nic")]
            public string Nic { get; set; }
        }
        
        
        public class CompanyDetailsModel
        {
            [Display(Name = "Company Name")]
            public string Name { get; set; }
            
            [Display(Name = "Company Registration No")]
            public string Crn { get; set; }
        }
        
        public class ContactDetailsModel
        {
            [Required]
            [PhoneNumber]
            [Display(Name = "Phone")]
            public string Phone { get; set; }
            
            [Required]
            public Address Address { get; set; } = new();
        }

        public class Address
        {
            [Required]
            [Display(Name = "Building / Home / Apartment")]
            public string Line1 { get; set; }
            
            [Required]
            [Display(Name = "Street")]
            public string Street { get; set; }
            
            [Required]
            [Display(Name = "City")]
            public string City { get; set; }
            
            [Required]
            [Display(Name = "District")]
            public string District { get; set; }
            
            [Required]
            [Display(Name = "ZipCode")]
            public string ZipCode { get; set; }
        }

        public class LoginDetailsModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }
            
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        #endregion
        
        public class InputModel
        {
            public PersonalDetailsModel PersonalDetails { get; set; } = new();
            public ContactDetailsModel ContactDetails { get; set; } = new();
            public LoginDetailsModel LoginDetails { get; set; } = new();
        }

        public Task OnGetAsync(string returnUrl = null)
        {
            var model = new InputModel
            {
                PersonalDetails =
                {
                    FirstName = "",
                    LastName = "",
                    MiddleName = "",
                    Nic = ""
                },
                ContactDetails =
                {
                    Address =
                    {
                        Line1 = "",
                        City = "",
                        District = "",
                        Street = "",
                        ZipCode = ""
                    }
                },
                LoginDetails =
                {
                    Email = "",
                    Password = "",
                    ConfirmPassword = ""
                }
            };
            Input = model;
            ReturnUrl = returnUrl;
            
            return Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            if (ModelState.IsValid)
            {
                var email = Input.LoginDetails.Email;
                var password = Input.LoginDetails.Password;
                
                var user = CreateReporter();

                await _emailStore.SetEmailAsync(user, email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private ReporterUser CreateReporter()
        {
            try
            {
                var reporter = new PersonReporter()
                {
                    Email = Input.LoginDetails.Email,
                    Name = new Name()
                    {
                        First = Input.PersonalDetails.FirstName,
                        Middle = Input.PersonalDetails.MiddleName,
                        Last = Input.PersonalDetails.LastName
                    },
                    Nic = new Nic(Input.PersonalDetails.Nic),
                    PhoneNumber = Input.ContactDetails.Phone,
                    Address = _mapper.Map<Domain.ValueObjects.Address>(Input.ContactDetails.Address)
                };
                
                return new ReporterUser(reporter);
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ReporterUser)}'. " +
                    $"Ensure that '{nameof(ReporterUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<ReporterUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ReporterUser>)_userStore;
        }
    }
}
