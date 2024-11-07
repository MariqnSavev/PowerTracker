using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BoxingAppDiploma.Data;
using BoxingAppDiploma.Models;

var builder = WebApplication.CreateBuilder(args);

// Добавяме контекста на базата данни и услугите на Identity
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Добавяме услуги за Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Добавяме контролерите с изгледи и Razor компилация
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

var app = builder.Build();

// Конфигуриране на HTTP заявките
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();  // Миграции в режим на разработка
}
else
{
    app.UseExceptionHandler("/Home/Error");  // Обработване на грешки в производствена среда
    app.UseHsts();  // Добавяме HTTP Strict Transport Security
}

app.UseHttpsRedirection();  // Пренасочване към HTTPS
app.UseStaticFiles();  // Статични файлове (CSS, JS, изображения)

app.UseRouting();  // Маршрутиране на заявки

app.UseAuthentication();  // Удостоверяване
app.UseAuthorization();  // Авторизация

// Добавяме маршрута за контролера по подразбиране
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Добавяме Razor Pages, ако са нужни
app.MapRazorPages();

// Стартираме приложението
app.Run();