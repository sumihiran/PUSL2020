using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using PUSL2020.Application.Identity;
using PUSL2020.Application.Identity.Models;

namespace PUSL2020.Web.Identity;

public class EmployeeSignInManager : SignInManager<EmployeeUser>
{
    public EmployeeSignInManager(
        UserManager<EmployeeUser> userManager,
        IHttpContextAccessor contextAccessor,
        IUserClaimsPrincipalFactory<EmployeeUser> claimsFactory,
        IOptions<IdentityOptions> optionsAccessor,
        ILogger<SignInManager<EmployeeUser>> logger,
        IAuthenticationSchemeProvider schemes,
        IUserConfirmation<EmployeeUser> confirmation
    ) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
    {
    }

    public override string AuthenticationType => ApplicationIdentityConstants.EmployeeAuthenticationScheme;
}