using Microsoft.AspNetCore.Authorization;
using PUSL2020.Application.Identity;

namespace PUSL2020.Application.Authorization;

public class EmployeeAuthorizeAttribute : AuthorizeAttribute
{
    public EmployeeAuthorizeAttribute()
    {
        AuthenticationSchemes = EmployeeIdentityConstants.AuthenticationScheme;
    }
}