using Microsoft.AspNetCore.Mvc;

namespace BoxingAppDiploma.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(); // Зарежда Index.cshtml
        }
        public IActionResult Tranings()
        {
            return View(); 
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}