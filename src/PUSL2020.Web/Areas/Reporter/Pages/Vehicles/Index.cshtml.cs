using AutoMapper;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PUSL2020.Application.Identity.Models;
using PUSL2020.Application.Services;

namespace PUSL2020.Web.Areas.Reporter.Pages.Vehicles;

[Authorize]
public class IndexModel : PageModel
{
    private readonly IMapper _mapper;
    private readonly UserManager<ReporterUser> _userManager;
    private readonly IVehicleService _vehicleService;

    public IndexModel(IVehicleService vehicleService, IMapper mapper, UserManager<ReporterUser> userManager)
    {
        _vehicleService = vehicleService;
        _mapper = mapper;
        _userManager = userManager;
    }

    public class VehicleModel
    {
        public string Vrn { get; set; } 
        public string Make { get; set; }
        
        public string VModel { get; set; }
        
        public string? EditUrl { get; set; }
    }

    public Task OnGetAsync()
    {
        return Task.CompletedTask;
    }

    public async Task<JsonResult> OnPostReadAsync([DataSourceRequest] DataSourceRequest request)
    {
        var reporter = await _userManager.FindByEmailAsync(User.Identity!.Name);
        var vehicles = await _vehicleService.GetVehiclesByReporterIdAsync(reporter.Id);
        var models = _mapper.Map<IEnumerable<VehicleModel>>(vehicles)
            .Select(vm =>
            {
                vm.EditUrl = Url.Page("/Vehicles/Edit", new { vrn = vm.Vrn,  area = "Reporter" });
                return vm;
            });
            
        return new JsonResult(await models.ToDataSourceResultAsync(request));
    }
}