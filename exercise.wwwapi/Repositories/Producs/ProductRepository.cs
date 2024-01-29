using exercise.wwwapi.Data;
using exercise.wwwapi.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Repositories.Producs
{
    public class ProductRepository : IProductRepository
    {

        private ProductContext _db;
        private int _id = 0;

        public ProductRepository(ProductContext db)
        {
            _db = db;
        }

        public async Task<Product> AddProduct(ProductPostPayload payload)
        {
            try
            {
                if (!isValidPostPayload(payload))
                {
                    return null;
                }

                var newProduct = new Product() { Id = getNextId(), Name = payload.name, Category = payload.category, Price = payload.price };
                _db.Add(newProduct);
                await _db.SaveChangesAsync();
                return newProduct;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex}");
                return null;
            }

        }

        private bool isValidPostPayload(ProductPostPayload payload)
        {
            try
            {
                if (!(payload.name is string) || !(payload.category is string))
                {
                    throw new Exception("Name and category must be strings.");
                }

                if (string.IsNullOrEmpty(payload.name) || string.IsNullOrEmpty(payload.category))
                {
                    throw new Exception("Name and category must be provided.");
                }

                if (!(payload.price > 0))
                {
                    throw new Exception("Price must be greater than 0");
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private int getNextId()
        {
            return _id++;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var deletedProduct = await getProductById(id);
            if (deletedProduct == null)
            {
                return false;
            }
            _db._products.Remove(deletedProduct);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<Product>> getAllProducts()
        {
            var result = await _db._products.ToListAsync();
            return result;
        }

        public async Task<Product?> getProductById(int id)
        {
            var result = await _db._products.FirstOrDefaultAsync(p => p.Id == id);
            return result;
        }

        public async Task<Product> UpdateProduct(int _id, ProductPutPayload payload)
        {
            var updatedProduct = await getProductById(_id);
            if (updatedProduct == null)
            {
                return null;
            }


            bool isUpdated = false;

            if (payload.name != null && payload.name.Length > 0)
            {
                updatedProduct.Name = payload.name;
                isUpdated = true;
            }

            if (payload.category != null && payload.category.Length > 0)
            {
                updatedProduct.Category = payload.category;
                isUpdated = true;
            }

            if (payload.price != null && payload.price.Value > 0)
            {
                updatedProduct.Price = payload.price.Value;
                isUpdated = true;
            }

            if (!isUpdated)
            {
                throw new Exception("No update payload entered");
            }

            await _db.SaveChangesAsync();

            return updatedProduct;
        }

        public Task<bool> AttachDiscountToProduct(int product_id, int discount_id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveDiscountFromProduct(int product_id)
        {
            throw new NotImplementedException();
        }
    }
}
