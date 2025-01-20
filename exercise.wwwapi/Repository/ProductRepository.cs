using exercise.wwwapi.Data;
using exercise.wwwapi.Models;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Repository
{
    public class ProductRepository : IRepository
    {
        private DataContext _db;
        public ProductRepository(DataContext db)
        {
            _db = db;
        }
        public async Task<Product> Add(Product product)
        {
            await _db.Products.AddAsync(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task<Product> Delete(int id)
        {
            var target = await _db.Products.FirstOrDefaultAsync(x => x.Id == id);
            _db.Products.Remove(target);
            _db.SaveChanges();
            return target;
        }

        public async Task<IEnumerable<Product>> GetAll(string? category)
        {
            var result = await _db.Products.Where(x => string.IsNullOrEmpty(category) || x.Category == category).ToListAsync();
            return result;
        }

        public async Task<Product> GetById(int id)
        {
            var result = await _db.Products.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }
        public async Task<Product> GetByName(string name)
        {
            var result = await _db.Products.FirstOrDefaultAsync(x => x.Name == name);
            return result;
        }
        public async Task<Product> Update(Product product)
        {
            _db.Products.Attach(product);
            _db.Entry(product).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return product;
        }
    }
}
