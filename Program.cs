using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PowerTracker.Data;

var builder = WebApplication.CreateBuilder(args);

// 📌 Добавяне на връзка с базата данни
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                        ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// 📌 Настройки за Identity (добавяне на поддръжка за роли)
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // Потребителите могат да влизат без потвърждение на имейл
})
.AddRoles<IdentityRole>() // Добавяне на поддръжка за роли
.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

// 📌 Инициализация на ролите при стартиране на приложението
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Admin", "User" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

// 📌 Създаване на администраторски акаунт (по желание)
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    string email = "admin@powertracker.com";
    string password = "Admin@123";

    if (await userManager.FindByEmailAsync(email) == null)
    {
        var adminUser = new IdentityUser
        {
            UserName = email,
            Email = email,
        };

        await userManager.CreateAsync(adminUser, password);
        await userManager.AddToRoleAsync(adminUser, "Admin");
    }
}

app.Run();