using exercise.wwwapi.DB;
using exercise.wwwapi.Models;

using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Repositories;

public class ProductRepository : IRepository<Product>
{
    private ProductContext _ctx;
    
    public ProductRepository(ProductContext ctx)
    {
        _ctx = ctx;
    }
    
    public Product Create(Product entity)
    {
        _ctx.Products.Add(entity);
        _ctx.SaveChanges();
        return entity;
    }

    public Product? Read(Guid id)
    {
        return _ctx.Products.Find(id);
    }

    public Product Update(Product entity)
    {
        _ctx.Entry(entity).State = EntityState.Modified;
        _ctx.SaveChanges();
        return entity;
    }

    public void Delete(Guid id)
    {
        var product = _ctx.Products.Find(id);
        
        _ctx.Products.Remove(product);
        _ctx.SaveChanges();
    }

    public Product? GetByName(string name)
    {
        return _ctx.Products.FirstOrDefault(p => p.Name == name);
    }

    public List<Product> GetAll()
    {
        return _ctx.Products.ToList();
    }
    
    public List<Product> GetAll(string category)
    {
        return _ctx.Products.Where(p => p.Category == category).ToList();
    }
    
    
    public void Detach(Product entity)
    {
        _ctx.Entry(entity).State = EntityState.Detached;
    }
}