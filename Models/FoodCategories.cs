using PowerTracker.Models;
using System.ComponentModel.DataAnnotations;

namespace PowerTracker.Models
{
    public class FoodCategories
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Foods> Foods { get; set; } = new List<Foods>();
    }
}