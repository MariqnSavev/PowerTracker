using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PowerTracker.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // Административен панел
        public IActionResult Index()
        {
            return View();
        }

        // Управление на потребители
        public IActionResult ManageUsers()
        {
            // Логика за управление на потребители
            return View();
        }
    }

}
