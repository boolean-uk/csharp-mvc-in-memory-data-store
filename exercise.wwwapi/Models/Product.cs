namespace exercise.wwwapi.Models
{
    // Public class for Product model with properties and constructor
    public class Product
    {
        public required string Name { get; set; }

        public required string Category { get; set; }

        public int Price { get; set; }
        public int Id { get; set; }
    
    }
    
}
