using Microsoft.EntityFrameworkCore;
using exercise.wwwapi.Data;
using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public class ProductsRepository : IRepository
    {

        private DataContext _db;

        public ProductsRepository(DataContext db)
        {
           _db = db;

        }

        public async Task<Product> CreateProduct(Product entity)

        {
            await _db.Products.AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity;

        }

        public async Task<Product> GetProduct(int id)
        {
            return await _db.Products.FindAsync(id);


        }

        //support-method for 400 response "The provided name already exists"
        public async Task<Product> GetProductByName(string name)
        {
            return await _db.Products.FirstOrDefaultAsync(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }


        public async Task<IEnumerable<Product>> GetAllProducts(string category)

        {

            if (category == null)
            {
                return await _db.Products.ToListAsync();
            }

            else
            {
                var targets = await _db.Products.Where(x=> x.Category ==category).ToListAsync();
                return targets;

            }
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var productToDelete = await _db.Products.FindAsync(id);

            _db.Products.Remove(productToDelete);
            await _db.SaveChangesAsync();
            return true;
        
        }

        public async Task<Product> UpdateProduct(int id, Product updatedProduct)
        {
            var productToUpdate = await _db.Products.FindAsync(id);

            productToUpdate.Name = updatedProduct.Name;
            productToUpdate.Price = updatedProduct.Price;
            productToUpdate.Category = updatedProduct.Category;
         
            await _db.SaveChangesAsync();
            return productToUpdate;



        }





    }
}
