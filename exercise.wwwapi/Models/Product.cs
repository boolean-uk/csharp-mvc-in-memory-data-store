using System.ComponentModel.DataAnnotations;

public class Product
{
    [Key]
    public Guid UUID { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public int Price { get; set; }

    public Product(string name, string category, int price)
    {
        UUID = Guid.NewGuid();
        Name = name;
        Category = category;
        Price = price;
    }
}
