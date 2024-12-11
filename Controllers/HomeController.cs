using Microsoft.AspNetCore.Mvc;
using PowerTracker.Data;
using PowerTracker.Models;
using System.Linq;

namespace PowerTracker.Controllers
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