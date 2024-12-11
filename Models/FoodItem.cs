namespace PowerTracker.Models
{
    public class FoodItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double CaloriesPer100g { get; set; }
        public double QuantityInGrams { get; set; }
        public double TotalCalories { get; set; } // Добавен setter
    }
}