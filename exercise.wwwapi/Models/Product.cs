namespace exercise.wwwapi.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int Price { get; set; }

        public Product(string name, string category, int price)
        {
            Name = name;
            Category = category;
            Price = price;
        }
    }
}
