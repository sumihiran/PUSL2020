using Microsoft.AspNetCore.Authorization;
using PUSL2020.Application.Identity;

namespace PUSL2020.Application.Authorization;

public class AdminAuthorizeAttribute : AuthorizeAttribute
{
    public AdminAuthorizeAttribute()
    {
        AuthenticationSchemes = ApplicationIdentityConstants.WebMasterAuthenticationScheme;
    }
}