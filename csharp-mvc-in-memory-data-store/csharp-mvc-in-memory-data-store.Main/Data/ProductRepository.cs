using mvc_in_memory_data_store.Data;
using System;

namespace mvc_in_memory_data_store.Models
{
    public class ProductRepository : IProductRepository
    {
        private static int IdCounter = 1;
        private static List<Product> _products = new List<Product>();

        //public void create(string name, string category, int price)
        //{
        //    Product product = new Product();
        //    product.id = ProductRepository.IdCounter++;
        //    product.Name = name;
        //    product.Category = category;
        //    product.Price = price;
        //    ProductRepository._products.Add(product);
        //}

        public List<Product> GetAll()
        {
            return ProductRepository._products; 
        }

        public Product GetById(int id)
        {
            return ProductRepository._products.First(book => book.id == id);
        }

        public bool AddProduct(Product product)
        {
            if (product != null)
            {
                product.id = ProductRepository.IdCounter++;
                _products.Add(product);
                return true;
            }
            return false;
        }
    }
}
