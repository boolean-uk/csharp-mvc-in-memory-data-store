using exercise.wwwapi.Model;
using System.Data;

namespace exercise.wwwapi.Controller
{
    public static class ProductController
    {
        public static List<Product> products = new List<Product>()
        {
            new Product() {Id = 1, Name = "How To", Category = "Book", price = 1000}
        };

        public static Product CreateProduct(Product product)
        {
            products.Add(product);
            return product;
        }

        public static List<Product> GetAll()
        {
            return products.ToList();
        }

        public static Product Get(int id)
        {
            Product product = products.FirstOrDefault(x => x.Id == id);
            return product;
        }
        public static Product Update(Product newProduct, int id)
        {
            Product product = Get(id);
            product.Id = newProduct.Id;
            product.Name = newProduct.Name;
            product.Category = newProduct.Category;
            product.price = newProduct.price;
            return product;
        }
    }
}
