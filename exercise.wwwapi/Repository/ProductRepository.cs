using exercise.wwwapi.Models;
using exercise.wwwapi.Data;
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
        public async Task<IEnumerable<Product>> Add(Product entity)
        {
            await _db.AddAsync(entity);
            await _db.SaveChangesAsync();
            return await GetAll();
        }
        public async Task<IEnumerable<Product>> Delete(int id)
        {
            var target = await _db.Products.FindAsync(id);
            _db.Products.Remove(target);
            await _db.SaveChangesAsync();
            return await GetAll();
        }

        public async Task<Product> Get(int id)
        {
            return await _db.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _db.Products.ToListAsync();
        }

        public async Task<IEnumerable<Product>> Update(int id, string name, string category, int price)
        {
            var target = await _db.Products.FindAsync(id);
            target.name = name;
            target.category = category;
            target.price = price;
            await _db.SaveChangesAsync();
            return await GetAll();
        }


    }
}
