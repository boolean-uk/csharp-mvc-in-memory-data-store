using exercise.wwwapi.Models;
using exercise.wwwapi.Models.Data;

namespace exercise.wwwapi.Repository
{
    public class ProductRepository : IProductRepository
    {
        public Product AddProduct(Product product)
        {
            return ProductCollection.AddProduct(product);
        }

        public Product DeleteProduct(int id)
        {
            return ProductCollection.DeleteProduct(id);

        }

        public List<Product> GetAll()
        {
            return ProductCollection.GetProducts();
        }

        public Product GetProduct(int id)
        {
            return ProductCollection.GetProduct(id);
        }

        public Product UpdateProduct(int id, Product product)
        {
            return ProductCollection.UpdateProduct(id, product);
        }
    }
}
