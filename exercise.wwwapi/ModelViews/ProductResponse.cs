using System;
using System.Data;
using exercise.wwwapi.Models;

namespace exercise.wwwapi.ModelViews;

public class ProductResponse
{
    public DateTime when {get;set;} = DateTime.Now;
    public string Status {get;set;} = "Deleted";
    public string? Name {get;set;}
    public string? Category {get;set;}
    public decimal? Price {get;set;}

    public ProductResponse(Product prod)
    {
        Name = prod.Name;
        Category = prod.Category;
        Price = prod.Price;
    }

    public ProductResponse(string status)
    {
        Status = status;
    }
}
