using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PUSL2020.Application.Authorization;
using PUSL2020.Application.Identity.Models;
using PUSL2020.Infrastructure.Data;
using PUSL2020.Web.Areas.Admin.ViewModels;

namespace PUSL2020.Web.Areas.Admin.Controllers;

[Controller]
[AdminAuthorize]
[Area("Admin")]
[Route("/Admin")]
public class EmployeeController : Controller
{
    private readonly ApplicationDbContext _context;

    public EmployeeController(ApplicationDbContext context)
    {
        _context = context;
    }

    private DbSet<EmployeeUser> Employees => _context.EmployeeUsers;

    [HttpGet]
    public IActionResult Index()
    {
        return View("Index");
    }
    
    [HttpPost]
    public async Task<ActionResult> Employee_Read([DataSourceRequest] DataSourceRequest request)
    {
        return Json(await Employees.AsQueryable().Select(e => new EmployeeViewModel()
        {
            Id = e.Id.ToString(),
            DisplayName = e.Employee!.DisplayName,
            Office = e.Employee!.Office.Name,
            Username = e.UserName
        }).ToDataSourceResultAsync(request));
    }

    [HttpPut]
    public ActionResult Employee_Create([DataSourceRequest] DataSourceRequest request, EmployeeViewModel employee)
    {
        throw new NotImplementedException();
    }
    
    [HttpPatch]
    public ActionResult Employee_Update([DataSourceRequest] DataSourceRequest request, EmployeeViewModel employee)
    {
        throw new NotImplementedException();
    }
    
    [HttpDelete]
    public ActionResult Employee_Destroy([DataSourceRequest] DataSourceRequest request, EmployeeViewModel employee)
    {
        throw new NotImplementedException();
    }
}