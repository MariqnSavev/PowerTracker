namespace PowerTracker.Models
{
    public class Foods
    {
        public int Id { get; set; }
        public double Name { get; set; }
        public string CaloriesPer100g { get; set; }
        public virtual FoodCategories NameOfcategorie { get; set; }
        public int IdCategorie { get; set; }
    }
}
