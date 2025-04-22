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


var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<AppUser>>();

    string username = "seasonic";
    string email = "amdfx9590@bk.ru";
    string password = "KaliL!nux2OOO";

    var user = await userManager.FindByNameAsync(username);
    if (user == null)
    {
        var newUser = new AppUser
        {
            UserName = username,
            Email = email,
            EmailConfirmed = true
        };
        var result = await userManager.CreateAsync(newUser, password);

        if (result.Succeeded)
        {
            Console.WriteLine(" Пользователь создан: admin / Admin123!");
        }
        else
        {
            Console.WriteLine(" Ошибки при создании:");
            foreach (var error in result.Errors)
                Console.WriteLine($" - {error.Description}");
        }
    }
}



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
