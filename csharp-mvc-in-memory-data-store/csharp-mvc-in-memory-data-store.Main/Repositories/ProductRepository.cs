using mvc_in_memory_data_store.Data;
using mvc_in_memory_data_store.Models;
using System;
using System.Runtime.ConstrainedExecution;

namespace mvc_in_memory_data_store.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private static int IdCounter = 1;
        private static List<Product> _products = new List<Product>(); 

        public void create(int Id, string Name, string Category, decimal Price)
        {
            Product bagel = new Product() {Id=1, Name="How to build APIs", Category="Book", Price=1500m };
            _products.Add(bagel);
        }

        public List<Product> findAll()
        {
            return _products;

        }

        public Product find(int id)
        {
            return _products.First(p => p.Id == id);
        }

        public bool Add(Product product)
        {
            if (product != null)
            {
                _products.Add(product);
                return true;
            }
            return false;
        }

        public bool GetProduct(int id, out Product? product)
        {
            product =  ProductDataStore.Products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                return true;
            }
            return false;
        }
    }
}
