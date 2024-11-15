using Microsoft.AspNetCore.Mvc;
using BoxingAppDiploma.Data;
using BoxingAppDiploma.Models;
using System.Linq;

namespace BoxingAppDiploma.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new HomePageViewModel
            {
                Trainings = _context.Training.OrderByDescending(t => t.Date).Take(5).ToList(), // Последни 5 тренировки
                Diets = _context.Diet.OrderByDescending(d => d.Date).Take(5).ToList() // Последни 5 диети
            };

            return View(model);
        }
    }
}