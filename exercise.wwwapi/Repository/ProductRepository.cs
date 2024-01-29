using exercise.wwwapi.Data;
using exercise.wwwapi.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace exercise.wwwapi.Repository
{
    public class ProductRepository : IProductRepository
    {
        private ProductContext _db;
        private int _id = 0;

        public ProductRepository(ProductContext db)
        {
            this._db = db;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            var tasks = _db.Products.ToListAsync();
            return await tasks;
        }

        public async Task<Product> AddProduct(string name, string catagory, int price)
        {
            List<Product> products = _db.Products.ToList();
            int nrProducts = await _db.Products.CountAsync();

            for (int i = 0; i < nrProducts; i++)
            {
                if (products[i].Name == name)
                    return null;
            }

            var newProduct = new Product() { ID = _id++, Name = name, Catagory = catagory, Price = price };
            await _db.AddAsync(newProduct);
            await _db.SaveChangesAsync();
            return newProduct;
        }

        public async Task<ProductDeal> AddDealToTask(int id, string text)
        {
            var newDeal = new ProductDeal() { ProductId = id, Description = text};
            await _db.AddAsync(newDeal);
            await _db.SaveChangesAsync();
            return newDeal;
        }

        public async Task<Product>? GetProduct(int id)
        {
            var task = await _db.Products.FirstOrDefaultAsync(t => t.ID == id);
            return task;
        }

        public async Task<Product>? UpdateProduct(Product products, ProductUpdatePayload updateData)
        {
            bool hasUpdate = false;
            List<Product> productList = _db.Products.ToList();
            int nrProducts = await _db.Products.CountAsync();

            if (updateData.name != null)
            {
                for (int i = 0; i < nrProducts; i++)
                {
                    if (productList[i].Name == updateData.name)
                        return null;
                }

                products.Name = (string)updateData.name;
                hasUpdate = true;
            }

            if (updateData.price != null)
            {
                products.Price = (int)updateData.price;
                hasUpdate = true;
            }

            if (updateData.catagory != null)
            {
                products.Catagory = (string)updateData.catagory;
                hasUpdate = true;
            }

            if (!hasUpdate) throw new Exception("No update data provided");
            await _db.SaveChangesAsync();
            return products;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var product = GetProduct(id);
            if (product == null) return false;
            _db.Products.Remove(await product);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
