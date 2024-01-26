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
            Name = name;
            Category = category;
            Price = price;
        }

        public Product Update(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Category = product.Category;
            Price = product.Price;
            return this;
        }
    }
}

public class ProductParameters
{
    public string Name { get; set; }
    public string Category { get; set; }
    public int Price { get; set; }
}