using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using PUSL2020.Application;
using PUSL2020.Application.Identity;
using PUSL2020.Application.Identity.Models;
using PUSL2020.Application.Services;
using PUSL2020.Infrastructure;
using PUSL2020.Infrastructure.Data;
using PUSL2020.Web.Identity;

var builder = WebApplication.CreateBuilder(args);


// Authentication

builder.Services.AddAuthenticationDefaults()
    .AddCookie(EmployeeIdentityConstants.AuthenticationScheme, o =>
    {
        o.Cookie.Name = EmployeeIdentityConstants.AuthenticationScheme;
        o.LoginPath = EmployeeAuthenticationWebDefaults.LoginPath;
        o.LogoutPath = EmployeeAuthenticationWebDefaults.LogoutPath;
        o.AccessDeniedPath = EmployeeAuthenticationWebDefaults.AccessDeniedPath;
        o.ReturnUrlParameter = EmployeeAuthenticationWebDefaults.ReturnUrlParameter;
        o.Events = new CookieAuthenticationEvents
        {
            OnValidatePrincipal = SecurityStampValidator.ValidatePrincipalAsync
        };
    })
    .AddIdentityCookies();

builder.Services.AddReporterIdentity()
    .AddSignInManager<SignInManager<ReporterUser>>();
builder.Services.AddEmployeeIdentity()
    .AddSignInManager<EmployeeSignInManager>();

// Email
builder.Services.AddScoped<IEmailSender, LoggingEmailSender>();


builder.Services.RegisterInfrastructureServices(builder.Configuration, builder.Environment);


builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsProduction())
{
    using (var scope = app.Services.CreateScope())
    {
        var initializer = ActivatorUtilities.CreateInstance<ApplicationDbContextInitializer>(scope.ServiceProvider);
        await initializer.InitialiseAsync();
        await initializer.SeedAsync();
    }
    
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();

app.Run();