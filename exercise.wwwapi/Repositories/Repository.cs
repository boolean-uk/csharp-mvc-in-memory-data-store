using exercise.wwwapi.Data;
using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repositories;

public class Repository(DataContext db) : IRepository<Product>
{
    public Product Add(Product p)
    {
        db.Add(p);
        db.SaveChanges();
        return p;
    }

    public List<Product> GetAll()
    {
        return db.Products.ToList();
    }
    
    public List<Product> GetAll(string category)
    {
        return db.Products.Where(p => p.Category.ToLower().Equals(category.ToLower())).ToList();
    }

    public Product Get(int id)
    {
        return db.Products.FirstOrDefault(x => x.Id == id)!;
    }

    public Product Update(Product a, Product b)
    {
        a.Name = b.Name;
        a.Category = b.Category;
        a.Price = b.Price;
        db.SaveChanges();
        return a;
    }

    public Product Delete(Product p)
    {
        db.Remove(p);
        db.SaveChanges();
        return p;
    }
}