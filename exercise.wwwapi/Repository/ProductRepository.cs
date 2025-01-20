
using genericapi.api.Data;
using genericapi.api.Models;
using Microsoft.EntityFrameworkCore;

namespace genericapi.api.Repository
{
    public class ProductRepository(DataContext db) : IRepository<Product, Guid>
    {
        private DataContext _db = db;

        public async Task<Product> Add(Product entity)
        {
            await _db.Products.AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(Guid id)
        {
            Product pet = await Get(id);
            _db.Products.Remove(pet);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<Product> Get(Guid id)
        {
            Product? pet = await _db.Products.FindAsync(id);
            if (pet == null)
            {
                throw new ArgumentException("That ID does not exist!");
            }
            return pet;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _db.Products.ToListAsync();
        }

        public async Task<Product> Update(Product entity)
        {
            _db.Products.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
