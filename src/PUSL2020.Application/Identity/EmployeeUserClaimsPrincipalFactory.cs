using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using PUSL2020.Application.Identity.Models;

namespace PUSL2020.Application.Identity;

public class EmployeeUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<EmployeeUser>
{
    public EmployeeUserClaimsPrincipalFactory(
        UserManager<EmployeeUser> userManager,
        IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
    {
    }

    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(EmployeeUser user)
    {
        var userId = await UserManager.GetUserIdAsync(user);
        var userName = await UserManager.GetUserNameAsync(user);
        var id = new ClaimsIdentity(EmployeeIdentityConstants.AuthenticationScheme,
            Options.ClaimsIdentity.UserNameClaimType,
            Options.ClaimsIdentity.RoleClaimType);
        id.AddClaim(new Claim(Options.ClaimsIdentity.UserIdClaimType, userId));
        id.AddClaim(new Claim(Options.ClaimsIdentity.UserNameClaimType, userName));
        if (UserManager.SupportsUserEmail)
        {
            var email = await UserManager.GetEmailAsync(user);
            if (!string.IsNullOrEmpty(email))
            {
                id.AddClaim(new Claim(Options.ClaimsIdentity.EmailClaimType, email));
            }
        }
        if (UserManager.SupportsUserSecurityStamp)
        {
            id.AddClaim(new Claim(Options.ClaimsIdentity.SecurityStampClaimType,
                await UserManager.GetSecurityStampAsync(user)));
        }
        if (UserManager.SupportsUserClaim)
        {
            id.AddClaims(await UserManager.GetClaimsAsync(user));
        }
        return id;
    }
}