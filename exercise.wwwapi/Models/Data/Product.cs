namespace exercise.wwwapi.Models.Data
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int Price { get; set; }

        public Product(int id, string name, string category, int price)
        {
            Id = id;
            Name = name;
            Category = category;
            Price = price;
        }
    }
}
