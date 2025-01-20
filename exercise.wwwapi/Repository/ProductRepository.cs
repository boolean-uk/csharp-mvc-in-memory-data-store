using exercise.wwwapi.Data;
using exercise.wwwapi.Model;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Repository
{
    public class ProductRepository : IRepository
    {

        private DataContext db;

        public ProductRepository( DataContext db)
        {
            this.db = db;
        }

        public async Task<Product> CreateProduct(Product product)
        {
            await db.Products.AddAsync(product);
            await db.SaveChangesAsync();

            return product;
        }

        public async Task<bool> DeleteProduct(string id)
        {
                var product = await db.Products.FindAsync(id);
                if (product == null) throw new Exception("Product not found");

                db.Products.Remove(product);
                await db.SaveChangesAsync();
                return true;
            
     

        }

        public async Task<IEnumerable<Product>> GetAll(string? category)
        {
            if (category == null) return await db.Products.ToListAsync();

            return await db.Products.Where(x => x.Category.ToLower() == category.ToLower()).ToListAsync();    

        }

        public async Task<Product> GetProduct(string id)
        {
            return await db.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async void Save()
        {
            await db.SaveChangesAsync();
        }
  
    }
}
