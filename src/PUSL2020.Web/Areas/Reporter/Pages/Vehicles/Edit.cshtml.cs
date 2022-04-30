using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PUSL2020.Application.Identity.Models;
using PUSL2020.Application.Services;
using PUSL2020.Domain.Enums;

namespace PUSL2020.Web.Areas.Reporter.Pages.Vehicles
{
    [Authorize]
    public class EditModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Vrn { get; set; }
        
        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string Vrn { get; set; }
            public string Make { get; set; }
            public string Model { get; set; }
            public string Class { get; set; }
            public FuelType FuelType { get; set; }
            public string EngineNo { get; set; }
            public DateOnly RegisteredAt { get; set; }
            public string OwnerName { get; set; }
            public string OwnerPhone { get; set; }
            public string InsurancePolicyId { get; set; }
            public DateTime InsuranceStartAt { get; set; }
            public DateTime InsuranceExpiryAt  { get; set; }
        }
        
        private readonly IMapper _mapper;
        private readonly UserManager<ReporterUser> _userManager;
        private readonly IVehicleService _vehicleService;

        public EditModel(IVehicleService vehicleService, IMapper mapper, UserManager<ReporterUser> userManager)
        {
            _vehicleService = vehicleService;
            _mapper = mapper;
            _userManager = userManager;
        }
        
        public async Task<IActionResult> OnGetAsync()
        {
            var reporter = await _userManager.FindByEmailAsync(User.Identity!.Name);
            var vehicle = await _vehicleService.GetVehicleByReporterIdAndVrnAsync(reporter.Id, Vrn);
            if (vehicle == null)
            {
                return RedirectToPage("/Error");
            }

            Input = _mapper.Map<InputModel>(vehicle);

            return Page();
        }
    }
}
