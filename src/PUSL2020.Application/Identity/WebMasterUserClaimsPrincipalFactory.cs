using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using PUSL2020.Application.Identity.Models;

namespace PUSL2020.Application.Identity;

public class WebMasterUserClaimsPrincipalFactory: IUserClaimsPrincipalFactory<WebMaster>
{
    public WebMasterUserClaimsPrincipalFactory(
        UserManager<WebMaster> userManager,
        IOptions<IdentityOptions> optionsAccessor)
    {
        if (optionsAccessor == null || optionsAccessor.Value == null)
        {
            throw new ArgumentNullException(nameof(optionsAccessor));
        }
        UserManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        Options = optionsAccessor.Value;
    }

    
    public UserManager<WebMaster> UserManager { get; private set; }

    
    public IdentityOptions Options { get; private set; }
    
    public async Task<ClaimsPrincipal> CreateAsync(WebMaster user)
    {
        var userId = await UserManager.GetUserIdAsync(user);
        var userName = await UserManager.GetUserNameAsync(user);
        var id = new ClaimsIdentity(ApplicationIdentityConstants.WebMasterAuthenticationScheme,
            Options.ClaimsIdentity.UserNameClaimType,
            Options.ClaimsIdentity.RoleClaimType);
        id.AddClaim(new Claim(Options.ClaimsIdentity.UserIdClaimType, userId));
        id.AddClaim(new Claim(Options.ClaimsIdentity.UserNameClaimType, userName));

        return new ClaimsPrincipal(id);
    }
}