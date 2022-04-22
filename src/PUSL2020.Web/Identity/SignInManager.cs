using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using PUSL2020.Application.Identity;

namespace PUSL2020.Web.Identity;

public abstract class SignInManager<TUser> : Microsoft.AspNetCore.Identity.SignInManager<TUser> where TUser: class
{
    public abstract string AuthenticationType { get; }
    
    public SignInManager(
        UserManager<TUser> userManager, 
        IHttpContextAccessor contextAccessor,
        IUserClaimsPrincipalFactory<TUser> claimsFactory, 
        IOptions<IdentityOptions> optionsAccessor,
        ILogger<SignInManager<TUser>> logger, 
        IAuthenticationSchemeProvider schemes,
        IUserConfirmation<TUser> confirmation) :
        base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
    {
        
    }

    public override bool IsSignedIn(ClaimsPrincipal principal)
    {
        if (principal == null)
        {
            throw new ArgumentNullException(nameof(principal));
        }
        return principal.Identities.Any(i => i.AuthenticationType == AuthenticationType);
    }


    public override async Task SignInWithClaimsAsync(TUser user, AuthenticationProperties authenticationProperties,
        IEnumerable<Claim> additionalClaims)
    {
        var userPrincipal = await CreateUserPrincipalAsync(user);
        foreach (var claim in additionalClaims)
        {
            userPrincipal.Identities.First().AddClaim(claim);
        }
        await Context.SignInAsync(AuthenticationType,
            userPrincipal,
            authenticationProperties ?? new AuthenticationProperties());
    }

    public override async Task SignOutAsync()
    {
        await Context.SignOutAsync(AuthenticationType);
    }

    public override async Task RefreshSignInAsync(TUser user)
    {
        var auth = await Context.AuthenticateAsync(AuthenticationType);
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