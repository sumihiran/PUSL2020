using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using PUSL2020.Application;
using PUSL2020.Application.Identity;
using PUSL2020.Application.Services;
using PUSL2020.Infrastructure;
using PUSL2020.Infrastructure.Identity;
using PUSL2020.Infrastructure.Services;
using PUSL2020.Web.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add Application Services
builder.Services.RegisterApplicationServices(builder.Configuration);

// Add Infrastructure
builder.Services.RegisterInfrastructureServices(builder.Configuration, builder.Environment);

// Add Auth
var authBuilder =  new AuthenticationBuilder(builder.Services);

// Reporter User - defaultIdentity
authBuilder.AddIdentityCookies();

// Staff User
authBuilder.AddCookie(StaffAuthenticationDefaults.AuthenticationScheme, opt =>
{
    opt.SlidingExpiration = true;
    opt.Cookie.Name = StaffAuthenticationWebDefaults.CookiePrefix;
    opt.LoginPath = StaffAuthenticationWebDefaults.LoginPath;
    opt.LogoutPath = StaffAuthenticationWebDefaults.LogoutPath;
    opt.AccessDeniedPath = StaffAuthenticationWebDefaults.AccessDeniedPath;
    opt.ReturnUrlParameter = StaffAuthenticationWebDefaults.ReturnUrlParameter;
});

// Add Identity UI
var reporterIdentityBuilder = new IdentityBuilder(typeof(ReporterUser), builder.Services);
reporterIdentityBuilder.AddDefaultUI();

var staffIdentityBuilder = new IdentityBuilder(typeof(StaffUser), builder.Services);
staffIdentityBuilder
    .AddSignInManager();

builder.Services.AddTransient<IEmailSender, LoggingEmailSender>();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseDeveloperExceptionPage();
}
else 
{
    app.UseExceptionHandler("/Home/Error");
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