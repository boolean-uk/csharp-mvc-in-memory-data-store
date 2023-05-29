using mvc_in_memory_data_store.Models;

namespace mvc_in_memory_data_store.Data
{
    public class ProductRepository : IProductRepository
    {
        private static List<Product> products = new List<Product>();
        private static int productId = 1;

        public List<Product> GetProducts() 
        { 
            return products; 
        }

        public Product GetProductById(int id) 
        {
            return products.FirstOrDefault(i => i._id == id);
        }

        public bool AddProduct(Product product)
        {
            if (product != null)
            {
                product._id = productId++;
                products.Add(product);
                return true;
            }
            return false;
        }
        public bool DeleteProduct(int id) 
        {
            if(products.Any(i => i._id == id)) 
            {
                var product = products.FirstOrDefault(i => i._id == id);
                products.Remove(product);
                return true;
            }
            return false;
        }

        public bool UpdateProduct(Product product, int id) 
        {
            if (products.Any(i => i._id == id))
            {
                var newProduct = products.FirstOrDefault(i => i._id == id);
                if (newProduct != null)
                {
                    product._id = id;
                    newProduct._name = product._name;
                    newProduct._category = product._category;
                    newProduct._price = product._price;
                }
                return true;
            }
            return false;
        }
    }
}
