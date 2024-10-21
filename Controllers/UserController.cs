using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BoxingAppDiploma.Models;
using BoxingAppDiploma.ViewModels; // Път до модела ApplicationUser


namespace BoxingAppDiploma.Controllers
{
    [Authorize] // Ограничаваме достъпа до всички действия за логнати потребители
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        // Страница за показване на профила на потребителя
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User); // Вземи текущия логнат потребител
            return View(user); // Предай потребителя на изгледа
        }

        // Страница за редактиране на профила (GET)
        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var user = await _userManager.GetUserAsync(User); // Вземи текущия потребител
            var model = new EditProfileViewModel
            {
                Name = user.FullName, // Запълни модела с данни от потребителя
                Email = user.Email
            };

            return View(model); // Предай модела на изгледа
        }

        // Обработка на формата за редактиране на профила (POST)
        [HttpPost]
        public async Task<IActionResult> EditProfile(EditProfileViewModel model)
        {
            if (!ModelState.IsValid) // Проверка за валидност на данните
            {
                return View(model); // Връща на формата при невалидни данни
            }

            var user = await _userManager.GetUserAsync(User); // Вземи текущия потребител
            user.FullName = model.Name; // Актуализирай името
            user.Email = model.Email; // Актуализирай имейла

            var result = await _userManager.UpdateAsync(user); // Запази промените
            if (result.Succeeded)
            {
                ViewBag.Message = "Профилът е актуализиран успешно!";
                return RedirectToAction("Profile"); // Пренасочване към профила
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description); // Добави грешки в модела
            }

            return View(model); // Върни на формата при неуспешно обновление
        }
    }
}