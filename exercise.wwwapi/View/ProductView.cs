using exercise.wwwapi.Controller;
using exercise.wwwapi.Model;

namespace exercise.wwwapi.View
{
    public class ProductView : IProduct<Product>
    {
        public Product CreateProduct(Product product)
        {
            return ProductController.CreateProduct(product);
        }
        public Product Update(Product entity, int id)
        {
            return ProductController.Update(entity, id);
        }

        Product IProduct<Product>.Delete(int id)
        {
            return ProductController.Delete(id);
        }

        Product IProduct<Product>.Get(int id)
        {
            return ProductController.Get(id);
        }

        List<Product> IProduct<Product>.GetAll(string category)
        {
            return ProductController.GetAll(category);
        }
    }
}
