namespace PowerTracker.Models
{
    public class Foods
    {
        public int Id { get; set; } // Unique identifier for the food
        public string Name { get; set; } // Name of the food
        public double CaloriesPer100g { get; set; } // Calories per 100 grams
        public virtual FoodCategories NameOfCategorie { get; set; } // Link to food category
        public int FoodCategorieID { get; set; } // Category ID (foreign key)
    }
}
