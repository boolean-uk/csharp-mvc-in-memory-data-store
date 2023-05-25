using Microsoft.AspNetCore.Http.HttpResults;
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


        public Product UpdateProduct(Guid id, string name, string category, decimal price)
        {
            if(_products.Any(x => x.Id == id))
            {
                var p = _products.SingleOrDefault(x =>  x.Id == id);
                if(p != null)
                {
                    p.Name = name;
                    p.Category = category;
                    p.Price = price;
                    return p;
                }
            }
            return null;
        }


        
        public bool Delete(Guid id)
        {
            if (_products.Any(x => x.Id == id))
            {
                _products.RemoveAll(x => x.Id == id);
                return true;
            }
            return false;
            
        }

        

       

        
    }
}
