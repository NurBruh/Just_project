using Just_project.Admin.Models;
using Microsoft.Extensions.Configuration; // Add this using directive
using Microsoft.Extensions.DependencyInjection; // Add this using directive
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//#region Auth
//builder.Services.AddDbContext<AppIdentityDbContext>
//    (options => options.UseSqlServer(
//        builder.Configuration["ConnectionStrings:DefaultConnection"]));

//builder.Services.AddIdentity<AppUser, IdentityRole>()
//    .AddEntityFrameworkStores<AppIdentityDbContext>()     
//    .AddDefaultTokenProviders();


//builder.Services.ConfigureApplicationCookie(opts => opts.LoginPath = "/Account/Login");

//#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
