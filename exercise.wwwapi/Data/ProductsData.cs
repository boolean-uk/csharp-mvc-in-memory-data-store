using Microsoft.EntityFrameworkCore;
using exercise.wwwapi.Data;

public class ProductsData
{
    private readonly DataContext _db;

    public ProductsData(DataContext db)
    {
        _db = db;
    }

    public async Task<Product> AddAsync(Product product)
    {
        _db.Products.Add(product);
        await _db.SaveChangesAsync();
        return product;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _db.Products.ToListAsync();
    }

    public async Task<Product> GetAsync(Guid UUID)
    {
        return await _db.Products.FirstOrDefaultAsync(p => p.UUID == UUID);
    }

    public async Task<Product> UpdateAsync(Guid UUID, string? name, string? category, int? price)
    {
        Product product = await _db.Products.FirstOrDefaultAsync(p => p.UUID == UUID);
        if (product != null)
        {
            if (!string.IsNullOrEmpty(name))
            {
                product.Name = name;
            }

            if (!string.IsNullOrEmpty(category))
            {
                product.Category = category;
            }

            if (price != null && price != product.Price && price > 0)
            {
                product.Price = (int)price;
            }

            await _db.SaveChangesAsync();
        }

        return product;
    }

    public async Task<bool> DeleteAsync(Guid UUID)
    {
        Product product = await _db.Products.FirstOrDefaultAsync(p => p.UUID == UUID);

        if (product != null)
        {
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
            return true;
        }

        return false;
    }
}
