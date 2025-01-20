using Microsoft.EntityFrameworkCore;
using workshop.wwwapi.Data;
using workshop.wwwapi.Models;

namespace workshop.wwwapi.Repository
{
    public class PetRepository  : IRepository
    {
        private DataContext _db;

        public PetRepository(DataContext db)
        {
            _db = db;
        }
        public async Task<Product> AddProduct(Product entity)
        {       
            await _db.Products.AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity;
            
        }

        public async Task<bool> Delete(int id)
        {
            var target = await _db.Products.FindAsync(id);
            _db.Products.Remove(target);
            await _db.SaveChangesAsync();
            return true;


        }

        public async Task<Product> GetProduct(int id)
        {
            return await _db.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _db.Products.ToListAsync();
        }
    }
}
