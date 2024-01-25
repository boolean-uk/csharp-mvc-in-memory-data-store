
using exercise.wwwapi.Products;
using System.Xml.Linq;

namespace exercise.wwwapi.Data
{
    public class ProductCollections : IProduct
    {
        private static List<Product> _products = new List<Product>()
        {
            new Product(){ Id = 1, Name="Tesla Model 3", Category="whicle", Price= 1200},
            new Product(){Id = 2,  Name="KvikLunsj", Category="Chocolage", Price= 10},
        };


        public IEnumerable<Product> GetProducts()
        {
            return _products;
        }

        public Product Add(Product product)
        {
            _products.Add(product);
            return product;
        }


        public Product Update(int id, ProductPut product)
        {
            var target = _products.FirstOrDefault(product => product.Id == id);
            target.Name = product.Name;
            target.Price = product.Price;
            target.Category = product.Category;
            return target;
        }

        public bool Get(int id, out Product product)
        {
            product = _products.FirstOrDefault(product => product.Id == id);

            if (product == null)
            {
                return false;
            }

            return true;
        }

        public bool Delete(int id)
        {
            var index = _products.FindIndex(product => product.Id == id);
            if (index == -1)
                return false;

            _products.RemoveAt(index);
            return true;
        }
    }
    
}