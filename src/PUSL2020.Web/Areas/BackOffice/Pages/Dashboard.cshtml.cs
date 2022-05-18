using Microsoft.AspNetCore.Mvc.RazorPages;
using PUSL2020.Application.Authorization;

namespace PUSL2020.Web.Areas.BackOffice.Pages;

[StaffAuthorize]
public class Dashboard : PageModel
{
    public void OnGet()
    {
        
    }
}