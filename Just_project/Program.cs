using Just_project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.AspNetCore.Builder; 
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LocalizeDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 35))));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 35))));

builder.Services.AddDbContext<AppIdentityDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 35))));


builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AppIdentityDbContext>()
    .AddDefaultTokenProviders();


builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    
});



// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] {

        new CultureInfo("en-Us"),
        new CultureInfo("ru-RU"),
        new CultureInfo("kk-KZ")
    };
    options.DefaultRequestCulture = new RequestCulture("kk-KZ");
    
    options.SupportedUICultures = supportedCultures;

});


builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<Just_project.Filters.BlockInternetExplorerFilter>();
});



builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();


Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .MinimumLevel.Debug()
    .WriteTo.Seq("http://localhost:5341")
    .WriteTo.Console()
    .WriteTo.File("Logs/hotelatrlogs_.txt", rollingInterval: RollingInterval.Day)
    .Enrich.FromLogContext()
    .CreateLogger();



builder.Host.UseSerilog();


var app = builder.Build();




// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseRequestLocalization();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();
app.Use(async (context, next) =>
{
    context.Response.Headers.Remove("Server"); 
    context.Response.Headers.Add("X-Powered-By", "666");
    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Add("X-Frame-Options", "DENY");
    context.Response.Headers.Add("Referrer-Policy", "no-referrer");
    
    await next();
});


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "blog",
    pattern: "blog",
    defaults: new { controller = "Blog", action = "Index" });

app.MapControllerRoute(
    name: "contact",
    pattern: "contact",
    defaults: new { controller = "Contact", action = "Index" });

app.MapControllerRoute(
    name: "faqs",
    pattern: "faqs",
    defaults: new { controller = "Faqs", action = "Index" });

app.MapControllerRoute(
    name: "shop",
    pattern: "shop",
    defaults: new { controller = "Shop", action = "Index" });

app.MapControllerRoute(
    name: "about-us",
    pattern: "about-us",
    defaults: new { controller = "Home", action = "About" });

app.MapControllerRoute(
    name: "components",
    pattern: "components",
    defaults: new { controller = "Category", action = "Components" });

app.MapControllerRoute(
    name: "readypc",
    pattern: "readypc",
    defaults: new { controller = "Category", action = "Ready" });

app.MapControllerRoute(
    name: "officepc",
    pattern: "officepc",
    defaults: new { controller = "Category", action = "Office" });

app.MapControllerRoute(
    name: "gamingpc",
    pattern: "gamingpc",
    defaults: new { controller = "Category", action = "Gaming" });

app.MapControllerRoute(
    name: "service",
    pattern: "service",
    defaults: new { controller = "Category", action = "Service" });

app.MapControllerRoute(
    name: "custompc",
    pattern: "custompc",
    defaults: new { controller = "Category", action = "Custom" });




app.Run();
