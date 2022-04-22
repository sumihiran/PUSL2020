using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PUSL2020.Application.Authorization;
using PUSL2020.Application.Identity.Models;

namespace PUSL2020.Web.Areas.Admin.Pages;


[AdminAuthorize]
public class Dashboard : PageModel
{
    private readonly SignInManager<WebMaster> _signInManager;
    private readonly ILogger<Dashboard> _logger;

    public Dashboard(SignInManager<WebMaster> signInManager, ILogger<Dashboard> logger)
    {
        _signInManager = signInManager;
        _logger = logger;
    }

    public void OnGet()
    {
    }
}