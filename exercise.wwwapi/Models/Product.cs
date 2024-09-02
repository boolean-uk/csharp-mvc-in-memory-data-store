namespace exercise.wwwapi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int Price { get; set; }

        public Product(string name, string category, int price)
        {
            this.Name = name;
            this.Category = category;
            this.Price = price;
        }
    }
}
