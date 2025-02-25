using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PowerTracker.Models
{
    public class FoodCategories
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        // Връзка към храните
        public virtual ICollection<Foods> Foods { get; set; } = new List<Foods>();
    }
}
