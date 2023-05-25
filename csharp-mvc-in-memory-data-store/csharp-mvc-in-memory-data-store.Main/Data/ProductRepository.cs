using mvc_in_memory_data_store.Models;

namespace mvc_in_memory_data_store.Data
{
    public class ProductRepository : IProductRepository
    {
        private static List<Product> _products = new List<Product>();
        

        public Product Create(string name, string category, decimal price)
        {
            Product product = new Product(name, category, price);
            _products.Add(product);
            return product;
        }


        public List<Product> GetAll()
        {
            return _products;
        }

        public Product Get(Guid id)
        {
            return _products.FirstOrDefault(product => product.Id == id);
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



     
        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Product Find(Guid id)
        {
            throw new NotImplementedException();
        }

        public Product UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public List<Product> FindAll()
        {
            throw new NotImplementedException();
        }

       

        
    }
}
