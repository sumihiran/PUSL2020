using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using PUSL2020.Application.Services;
using PUSL2020.Application.Services.Impl;

namespace PUSL2020.Application;

public class ApplicationServiceBuilder
{
    private readonly IServiceCollection _services;

    public ApplicationServiceBuilder(IServiceCollection services)
    {
        services.AddSingleton<IClock, ApplicationClock>();
        services.AddSingleton<ISystemClock>(sp => (sp.GetService<IClock>() as ApplicationClock)!);
        
        _services = services;
    }

}