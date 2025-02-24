using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PowerTracker.Models
{
    public class Foods
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double CaloriesPer100g { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual FoodCategories Category { get; set; }
    }
}