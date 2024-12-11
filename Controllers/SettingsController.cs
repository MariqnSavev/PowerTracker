using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PowerTracker.Controllers
{
    [Authorize]
    public class SettingsController : Controller
    {
        // Страница за настройки
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveSettings(string theme, bool notificationsEnabled)
        {
            // Логика за записване на потребителските настройки
            ViewBag.Message = "Settings updated!";
            return RedirectToAction("Index");
        }
    }

}
