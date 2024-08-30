namespace exercise.wwwapi.Models;

public class PutProduct
{
    public Object Name { get; set; }
    public Object Category { get; set; }
    public Object Price { get; set; }
    
    
    public Product ToProduct()
    {
        return new Product($"{Name}", $"{Category}", int.Parse($"{Price}"))
        {
            Id = 0,
        };
    }
}