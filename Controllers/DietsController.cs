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
    public class DietsController : Controller
    {


        

      private readonly ApplicationDbContext _context;

        public DietsController(ApplicationDbContext context)
        {
            _context = context;
        }
        
       

        public IActionResult Index()
        {
            var diets = _context.Diet.ToList();
            return View(diets);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Diet diet)
        {
            if (ModelState.IsValid)
            {
                _context.Diet.Add(diet);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(diet);
        }
    }
}
