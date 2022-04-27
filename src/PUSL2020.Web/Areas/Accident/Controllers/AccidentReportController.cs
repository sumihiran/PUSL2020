using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PUSL2020.Web.Areas.Accident.Controllers;

[Area("Accident")]
[Controller]
[Route("Accident")]
[Authorize]
public class AccidentReportController : Controller
{
    [HttpGet]
    public IActionResult Report()
    {
        return View();
    }
}