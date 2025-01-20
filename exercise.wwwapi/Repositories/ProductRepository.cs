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

    public Product Read(Guid id)
    {
        throw new NotImplementedException();
    }

    public Product Update(Product entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public Product GetByName(string name)
    {
        throw new NotImplementedException();
    }

    public List<Product> GetAll()
    {
        return _ctx.Products.ToList();
    }
}