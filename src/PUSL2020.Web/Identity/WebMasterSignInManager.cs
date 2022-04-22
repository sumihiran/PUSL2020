using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using PUSL2020.Application.Identity;
using PUSL2020.Application.Identity.Models;

namespace PUSL2020.Web.Identity;

public class WebMasterSignInManager : SignInManager<WebMaster>
{
    public WebMasterSignInManager(
        UserManager<WebMaster> userManager, 
        IHttpContextAccessor contextAccessor,
        IUserClaimsPrincipalFactory<WebMaster> claimsFactory, 
        IOptions<IdentityOptions> optionsAccessor,
        ILogger<SignInManager<WebMaster>> logger, 
        IAuthenticationSchemeProvider schemes,
        IUserConfirmation<WebMaster> confirmation
        ) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
    {
    }

    public override string AuthenticationType => ApplicationIdentityConstants.WebMasterAuthenticationScheme;
}