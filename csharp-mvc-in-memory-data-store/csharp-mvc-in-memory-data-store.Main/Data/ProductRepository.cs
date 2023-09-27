using mvc_in_memory_data_store.Data;
using System;
using System.Runtime.ConstrainedExecution;

namespace mvc_in_memory_data_store.Models
{
    public class ProductRepository : IProductRepository
    {
        private static int IdCounter = 1;
        private static List<Product> _products = new List<Product>();

        public void create(string name, string category, int price)
        {
            Product product = new Product(ProductRepository.IdCounter++, name, category, price);
            ProductRepository._products.Add(product);
        }

        public List<Product> findAll()
        {
            return ProductRepository._products;
            
        }

        public Product find(int id)
        {
            return ProductRepository._products.First(product => product.getId() == id);
        }

        public bool Add(Product product)
        {
            if (product != null)
            {
                int id = _products.Count == 0 ? 1 : _products.Max(x => x.Id) + 1;
                product.Id = id;
                _products.Add(product);
                return true;
            }
            return false;
        }

        public void Delete(int id)
        {
            var item = find(id);
            if (item.Id == id)
            {
                _products.Remove(item);
            }
        }
    }
}
