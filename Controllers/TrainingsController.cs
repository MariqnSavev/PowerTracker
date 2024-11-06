using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BoxingAppDiploma.Data;
using BoxingAppDiploma.Models;

namespace BoxingAppDiploma.Controllers
{
    public class TrainingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrainingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var trainings = _context.Training.ToList();
            return View(trainings);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Training training)
        {
            if (ModelState.IsValid)
            {
                _context.Training.Add(training);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(training);
        }

    }
}
