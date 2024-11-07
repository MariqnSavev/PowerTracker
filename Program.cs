using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BoxingAppDiploma.Data;
using BoxingAppDiploma.Models;

var builder = WebApplication.CreateBuilder(args);

// Получаваме connection string от конфигурацията (например от appsettings.json)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                        ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Добавяме контекста на базата данни и настройка за EF Core с повторно изпълнение при грешки
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString, sqlOptions =>
        sqlOptions.EnableRetryOnFailure(  // Включване на механизма за повторни опити
            maxRetryCount: 5,             // Максимален брой опити
            maxRetryDelay: TimeSpan.FromSeconds(10),  // Максимално време за изчакване между опитите
            errorNumbersToAdd: null)      // Можеш да добавиш специфични грешки, ако е необходимо (за момента оставяме null)
    )
);

// Добавяме услугите за обработка на грешки в режима на разработка
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Добавяме услуги за ASP.NET Core Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Добавяме контролери с изгледи и Razor компилация
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

app.UseRouting();  // Маршрутиране на HTTP заявки

app.UseAuthentication();  // Удостоверяване (Authentication)
app.UseAuthorization();  // Авторизация (Authorization)

// Добавяме маршрута за контролера по подразбиране
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

// Добавяме Razor Pages (ако използваш Razor Pages, а не само MVC контролери)
app.MapRazorPages();

// Стартираме приложението
app.Run();