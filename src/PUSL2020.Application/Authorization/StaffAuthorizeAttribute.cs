using Microsoft.AspNetCore.Authorization;
using PUSL2020.Application.Identity;

namespace PUSL2020.Application.Authorization;

public class StaffAuthorizeAttribute : AuthorizeAttribute
{
    public StaffAuthorizeAttribute()
    {
        AuthenticationSchemes = StaffAuthenticationDefaults.AuthenticationScheme;
    }
}