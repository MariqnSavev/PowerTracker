using BoxingAppDiploma.Models;
using Microsoft.AspNetCore.Mvc;

namespace BoxingAppDiploma.Controllers
{
    public class CalorieCalculatorController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(new CalorieCalculator());
        }

        [HttpPost]
        public IActionResult Calculate(CalorieCalculator model)
        {
            if (ModelState.IsValid)
            {
                // Примерни стойности на MET (Metabolic Equivalent of Task)
                var metValues = new Dictionary<string, double>
            {
                { "Running", 9.8 },
                { "Cycling", 7.5 },
                { "Walking", 3.8 },
                { "Weightlifting", 6.0 }
            };

                if (metValues.ContainsKey(model.Activity))
                {
                    double met = metValues[model.Activity];
                    model.CaloriesBurned = (met * model.WeightInKg * model.DurationInMinutes) / 60;
                }
                else
                {
                    ModelState.AddModelError("", "Activity not found.");
                    return View("Index", model);
                }

                return View("Result", model);
            }

            return View("Index", model);
        }
    }
}
