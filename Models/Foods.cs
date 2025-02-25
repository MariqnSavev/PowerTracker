using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PowerTracker.Models
{
    public class Foods
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double CaloriesPer100g { get; set; }

        // Връзка към категория
        [ForeignKey("FoodCategories")]
        public int CategoryId { get; set; }
        public virtual FoodCategories Category { get; set; }
    }
}
