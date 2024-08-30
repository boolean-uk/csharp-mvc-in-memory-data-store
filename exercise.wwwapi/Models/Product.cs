namespace exercise.wwwapi.Models;

public class Product(string name, string category, int price)
{
    public int Id { get; set; }
    public string Name { get; set; } = name;
    public string Category { get; set; } = category;
    public int Price { get; set; } = price;
}