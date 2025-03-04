using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PowerTracker.Data;
using System.Linq;
using System.Threading.Tasks;

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
            return View();
        }

        /// 📌 Връща JSON с данните за графиката
        public async Task<JsonResult> GetProgressData()
        {
            var data = await _context.Goal
                .OrderBy(g => g.StartDate)
                .Select(g => new { g.StartDate, g.StartWeight })
                .ToListAsync();
            return Json(data);
        }
    }
}
