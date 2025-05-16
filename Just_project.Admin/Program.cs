using Just_project.Admin.Models;
using Microsoft.Extensions.Configuration; // Add this using directive
using Microsoft.Extensions.DependencyInjection; // Add this using directive
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


#region MYSQLCONNECTION
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 35))));

builder.Services.AddDbContext<AppIdentityDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 35))));
#endregion

#region Auth
//builder.Services.AddDbContext<AppIdentityDbContext>
//    (options => options.UseSqlServer(
//        builder.Configuration["ConnectionStrings:DefaultConnection"]));


builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AppIdentityDbContext>()
    .AddDefaultTokenProviders();


builder.Services.ConfigureApplicationCookie(opts => opts.LoginPath = "/Account/Login");

#endregion
builder.Services.AddHttpClient();


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
