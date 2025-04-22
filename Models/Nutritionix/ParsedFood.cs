using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PowerTracker.Models.Nutritionix
{
    public class ParsedFood
    {

        public string FoodName { get; set; }
        public string BrandName { get; set; }
        public double ServingQty { get; set; }
        public string ServingUnit { get; set; }
        public double ServingWeightGrams { get; set; }
        public double Calories { get; set; }
        public double TotalFat { get; set; }
        public double TotalCarbohydrate { get; set; }
        public double Protein { get; set; }
        public string Photo { get; set; }

        
    }
}