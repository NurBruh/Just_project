using Just_project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.AspNetCore.Builder; 
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Razor;

var builder = WebApplication.CreateBuilder(args);

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

string connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppIdentityDbContext>(
    options => options.UseSqlServer(connection));

builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AppIdentityDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie
    (option => option.LoginPath = "/Account/Login");

//builder.Services.AddControllersWithViews()
//    .AddViewLocalization()  
//    .AddDataAnnotationsLocalization();

//builder.Services.AddLocalization(options => options.ResourcesPath = "Resources")
//    .AddControllersWithViews()
//    .AddViewLocalization()
//    .AddDataAnnotationsLocalization();





//var supportedCultures = new[] {
//    "en-US",
//    "ru-RU",
//    "kk-KZ"
//};

//var localizationOptions = new RequestLocalizationOptions()
//    .AddSupportedCultures(supportedCultures).AddSupportedUICultures(supportedCultures);



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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
