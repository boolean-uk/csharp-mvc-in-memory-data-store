using System;
using exercise.wwwapi.Data;
using exercise.wwwapi.Model;
using exercise.wwwapi.View;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Repositories;

public class ProductRepository : IRepository
{
    private DataContext _db;

    public ProductRepository(DataContext db)
    {
        _db = db;
    }

    public async Task<Product> AddProduct(Product product)
    {
        await _db.Products.AddAsync(product);
        await _db.SaveChangesAsync();

        return product;
    }

    public async Task<bool> DeleteProduct(Guid id)
    {
        var target = await _db.Products.FindAsync(id);
        if (target == null)
        {
            return false;
        }

        _db.Products.Remove(target);
        await _db.SaveChangesAsync();
        return true;

    }

    public async Task<Product> GetProduct(Guid id)
    {
        var found = await _db.Products.FindAsync(id) ?? throw new KeyNotFoundException();
        return found;
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
        return await _db.Products.ToListAsync();
    }

    public async Task<Product> UpdateProduct(Guid Id, ProductPut productView)
    {
        var target = await _db.Products.FindAsync(Id) ?? throw new KeyNotFoundException();

        if (productView.Name != null) target.Name = productView.Name;
        if (productView.Price != null) target.Price = (decimal)productView.Price;
        if (productView.Category != null) target.Category = productView.Category;

        await _db.SaveChangesAsync();
        return target;
    }
}
