using exercise.wwwapi.Data;
using exercise.wwwapi.Models;
using exercise.wwwapi.ViewModel;
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

        public async Task<Product> Get(int id)
        {
            return await _db.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _db.Products.ToListAsync();
        }

        public async Task<Product> Add(Product entity)
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

        public async Task<Product> Update(int id, ProductViewModel entity)
        {
            var target = await _db.Products.FindAsync(id);
            target.name = entity.name;
            target.category = entity.category;
            target.price = entity.price;
            await _db.SaveChangesAsync();
            return target;
        }

    }
}
