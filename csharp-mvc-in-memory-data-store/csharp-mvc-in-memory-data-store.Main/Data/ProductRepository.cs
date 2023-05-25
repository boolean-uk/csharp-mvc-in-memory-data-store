using mvc_in_memory_data_store.Models;

namespace mvc_in_memory_data_store.Data
{
    public class ProductRepository : IProductRepository
    {
        private static int IdCounter = 1;
        private static List<Product> _products = new List<Product>();

        public void create(string name, string category, decimal price)
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
                _products.Add(product);
                return true;
            }
            return false;
        }

        public Product Update(int id, string name, string category, decimal price)
        {
            var product = _products.SingleOrDefault(x => x.Id == id);
            if (product != null)
            {
                if (!string.IsNullOrEmpty(name))
                {
                    product.Name = name;
                }
                if (!string.IsNullOrEmpty(category))
                {
                    product.Category = category;
                }
                if (!string.IsNullOrEmpty(price.ToString()))
                {
                    product.Price = (decimal)price;
                }
                return product;
            }
            else
            {
                return product;
            }
        }

        public bool Delete(int id)
        {
            var product = _products.SingleOrDefault(x => x.Id == id);
            if (product != null)
            {
                _products.Remove(product);
                return true;
            }
            return false;
        }
    }
}
