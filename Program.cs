using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PowerTracker.Data;
using PowerTracker.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Добавяне на базата данни
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// 2. Добавяне на Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>();

// 3. Добавяне на MVC и Razor Pages
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// 4. NutritionixService конфигурация
builder.Services.AddHttpClient<INutritionixService, NutritionixService>();

var app = builder.Build();

// 5. Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
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

// 6. Маршрутизация – задаваме начална страница към FoodsController
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Foods}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
