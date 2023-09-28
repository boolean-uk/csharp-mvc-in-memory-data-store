using mvc_in_memory_data_store.Data;
using System;
using System.Globalization;
using System.Runtime.ConstrainedExecution;

namespace mvc_in_memory_data_store.Models
{
    public class ProductRepository : IProductRepository
    {
        private static int IdCounter = 6;

        private static List<Product> _products = new List<Product>
        {
            new Product { id = 1, name = "Product 1", category = "book", price = 17 },
            new Product { id = 2, name = "Product 2", category = "book", price = 15 },
            new Product { id = 3, name = "Product 3", category = "notebook", price = 5 },
            new Product { id = 4, name = "Product 4", category = "notebook", price = 7 },
            new Product { id = 5, name = "Product 5", category = "pen", price = 3 },
        };

        public List<Product> GetAllProducts()
        {
            return _products;        
        }

        public Product GetProductById(int id)
        {
            return _products.FirstOrDefault(p => p.id == id);
        }

        public Product AddProduct(Product product)
        {
            product.id = IdCounter++;
            _products.Add(product);
            return product;
        }

        public Product DeleteProduct(int id)
        {
            var productToRemove = _products.FirstOrDefault(p => p.id == id);
            if (productToRemove != null)
            {
                _products.Remove(productToRemove);
                return productToRemove;
            }
            return null;
        }

        public Product UpdateProduct(int id, Product updatedProduct)
        {
            var existingProduct = _products.FirstOrDefault(p => p.id == id);
            if (existingProduct != null)
            {
                existingProduct.name = updatedProduct.name;
                existingProduct.category = updatedProduct.category;
                existingProduct.price = updatedProduct.price;
            }
            return existingProduct;
        }
    }
}