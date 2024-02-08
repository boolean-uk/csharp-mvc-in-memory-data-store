using exercise.wwwapi.Data;
using exercise.wwwapi.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace exercise.wwwapi.Repository
{
    //Create repositpry for all products
    public class ProductRepository:IproductRepository
    {

        private ProductContext _db;

        public ProductRepository(ProductContext db)
        {
            _db = db;
        }

        //create product
        public async Task<Product> CreateProduct(ProductPayload createData)
        {
            var name = createData.name;
            var category = createData.category;
            var price = createData.price;

            // it is not possible to enter something else than a int
            //for the price anyway

            // Check if a product with the same name already exists
            if (_db.Products.Any(p => p.Name == name))
            {
                return null; // Return null if a product with the same name already exists
            }

            var product = new Product() { Name = name, Category = category, Price = price };
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            return product;
        }

        //get all products
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _db.Products.ToListAsync();
        }

        //get product by ID
        public async Task<Product?> GetProductById(int id)
        {
            var product = await _db.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return null;
            }
            return product;
        }

        //get products by category
        public async Task<IEnumerable<Product?>> GetProductsByCategory(string category)
        {
            var productsToReturn = await _db.Products.Where(p => p.Category == category).ToListAsync();
            if (productsToReturn.Any())
            {
                return productsToReturn;
            }
            return null;
        }

        //update product
        public async Task<Product> UpdateProduct(int id, ProductPayload updateData)
        {
            var newName = updateData.name;
            var newCategory = updateData.category;
            var newPrice = updateData.price;

            if (_db.Products.Any(p => p.Name == newName))
            {
                return null; // Return null if a product with the same name already exists
            }

            var product = await GetProductById(id); 
            if (product == null)
            {
                return null; //return null if there is no product with this ID
            }
            
            else
            {
                product.Name = newName;
                product.Category = newCategory;
                product.Price = newPrice;
                await _db.SaveChangesAsync();
                return product;
            }
        }

        //delete product
        public async Task<Product> DeleteProduct(int id)
        {
            var product = await GetProductById(id);
            if (product == null)
            {
                return null;
            }
            else
            {
                _db.Products.Remove(product);
                await _db.SaveChangesAsync();
                return product;
            }
        }
    }
}
