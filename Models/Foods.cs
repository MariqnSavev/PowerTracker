namespace PowerTracker.Models
{
    public class Foods
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double CaloriesPer100g { get; set; }
        public virtual FoodCategories NameOfCategorie { get; set; }
        public int FoodCategorieID { get; set; }
    }
}
