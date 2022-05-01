using Microsoft.Extensions.DependencyInjection;
using PUSL2020.Application.Data;
using PUSL2020.Application.Data.Impl;
using PUSL2020.Application.Services;
using PUSL2020.Application.Services.Impl;

namespace PUSL2020.Application;

public class ApplicationServiceBuilder
{
    private readonly IServiceCollection _services;

    public ApplicationServiceBuilder(IServiceCollection services)
    {
        _services = services;
    }
    
    public ApplicationServiceBuilder AddRepositories()
    {
        _services.AddTransient<IReporterRepository, ReporterRepository>();
        _services.AddTransient<IVehicleRepository, VehicleRepository>();
        _services.AddTransient<IAccidentRepository, AccidentRepository>();
        return this;
    }
    
    public  ApplicationServiceBuilder AddServices()
    {
        _services.AddTransient<IVehicleService, VehicleService>();
        _services.AddTransient<IAccidentService, AccidentService>();
        return this;
    }

}