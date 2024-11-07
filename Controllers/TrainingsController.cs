using Microsoft.AspNetCore.Mvc;
using BoxingAppDiploma.Models;
using BoxingAppDiploma.Data;

namespace BoxingAppDiploma.Controllers
{
    public class TrainingController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Конструктор за инжектиране на контекста
        public TrainingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Training/Create
        public IActionResult Create()
        {
            return View();  // Търси изглед в Views/Training/Create.cshtml
        }

        // POST: Training/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Training training)
        {
            if (ModelState.IsValid)
            {
                _context.Training.Add(training);  // Добавяме тренировката в контекста
                _context.SaveChanges();  // Записваме промените в базата данни
                return RedirectToAction(nameof(Index));  // Пренасочваме към Index
            }
            return View(training);  // Ако има грешки, връщаме към формата
        }
    }
}
