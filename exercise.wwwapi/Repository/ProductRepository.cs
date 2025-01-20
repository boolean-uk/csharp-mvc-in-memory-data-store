namespace exercise.wwwapi.repository;

using exercise.wwwapi.data;
using exercise.wwwapi.model;
using exercise.wwwapi.viewmodel;
using Microsoft.EntityFrameworkCore;

public class ProductRepository : IRepository<Product, ProductPut>
{
    private DataContext _db;

    public ProductRepository(DataContext db)
    {
        this._db = db;
    }

    public async Task<Product> Add(Product entity)
    {
        _db.Products.Add(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task<Product?> Delete(int id)
    {
        var target = await _db.Products.FindAsync(id);
        if (target == null)
        {
            return null;
        }
        _db.Products.Remove(target);
        await _db.SaveChangesAsync();
        return target;
    }

    public async Task<Product?> Get(int id)
    {
        return await _db.Products.FindAsync(id);
    }

    public async Task<IEnumerable<Product>> GetAll()
    {
        return await this._db.Products.ToListAsync();
    }

    public async Task<IEnumerable<Product>?> GetSome(Predicate<Product> pred)
    {
        var products = await this.GetAll();
        var found = products.Where((p) => pred(p));
        if (found.Count() == 0)
        {
            return null;
        }
        return found;
    }

    public async Task<Product?> Update(int id, ProductPut partialEntity)
    {
        var target = await _db.Products.FindAsync(id);
        if (target == null)
        {
            return null;
        }
        if (partialEntity.Category != null)
        {
            target.Category = partialEntity.Category;
        }
        if (partialEntity.Name != null)
        {
            target.Name = partialEntity.Name;
        }
        if (partialEntity.Category != null)
        {
            target.Category = partialEntity.Category;
        }
        await _db.SaveChangesAsync();
        return target;
    }
}
