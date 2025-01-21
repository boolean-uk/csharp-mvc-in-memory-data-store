using exercise.wwwapi.Data;
using exercise.wwwapi.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace exercise.wwwapi.Repository
{
    public class ProductRepository
    {
        private DataContext  db;
        public ProductRepository(DataContext db) {
            this.db = db;
        }
        public async Task<Product> AddProduct(Product product)

        { 

           // var highestId = await db.Products.MaxAsync(p => p.Id);
           // product.Id = highestId + 1;
            await db.Products.AddAsync(product);
            await db.SaveChangesAsync();
            return product;
        }

        public async Task<Product> DeleteProduct(int id)
        {
            var target = await db.Products.FindAsync(id);
            db.Products.Remove(target);
            await db.SaveChangesAsync();
            return target;

        }
        public async Task<Product> GetProduct(int id )
        {
            var target = await db.Products.FindAsync(id);
            return target;

        }
        public async Task<IEnumerable<Product>> GetProducts(string category)
        {
              var target = await db.Products.Where(p => p.Category.ToLower() == category.ToLower()).ToListAsync();  

            if (target.IsNullOrEmpty())
            {
                return await db.Products.ToListAsync();

            }
            return target;
                 
        }
        public async Task<IEnumerable<Product>> GetAll()
        {
            return await db.Products.ToListAsync();

        }

        public async Task<Product> UpdateProduct(int id, string name, string category, int price )
        {
            var target = await db.Products.FindAsync(id);
            if (target != null)
            {
                target.Name = name;
                target.Category = category;
                target.Price = price;
            }

            return target;
            

        }

        

        


    }
}
