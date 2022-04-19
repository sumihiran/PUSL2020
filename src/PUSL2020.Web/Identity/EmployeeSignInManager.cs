using System.Security.Claims;
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
        IUserConfirmation<EmployeeUser> confirmation) :
        base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
    {
        
    }

    public override bool IsSignedIn(ClaimsPrincipal principal)
    {
        if (principal == null)
        {
            throw new ArgumentNullException(nameof(principal));
        }
        return principal.Identities.Any(i => i.AuthenticationType == EmployeeIdentityConstants.AuthenticationScheme);
    }


    public override async Task SignInWithClaimsAsync(EmployeeUser user, AuthenticationProperties authenticationProperties,
        IEnumerable<Claim> additionalClaims)
    {
        var userPrincipal = await CreateUserPrincipalAsync(user);
        foreach (var claim in additionalClaims)
        {
            userPrincipal.Identities.First().AddClaim(claim);
        }
        await Context.SignInAsync(EmployeeIdentityConstants.AuthenticationScheme,
            userPrincipal,
            authenticationProperties ?? new AuthenticationProperties());
    }

    public override async Task SignOutAsync()
    {
        await Context.SignOutAsync(EmployeeIdentityConstants.AuthenticationScheme);
    }

    public override async Task RefreshSignInAsync(EmployeeUser user)
    {
        var auth = await Context.AuthenticateAsync(EmployeeIdentityConstants.AuthenticationScheme);
        IList<Claim> claims = Array.Empty<Claim>();

        var authenticationMethod = auth?.Principal?.FindFirst(ClaimTypes.AuthenticationMethod);
        var amr = auth?.Principal?.FindFirst("amr");

        if (authenticationMethod != null || amr != null)
        {
            claims = new List<Claim>();
            if (authenticationMethod != null)
            {
                claims.Add(authenticationMethod);
            }
            if (amr != null)
            {
                claims.Add(amr);
            }
        }

        await SignInWithClaimsAsync(user, auth?.Properties, claims);
    }

}