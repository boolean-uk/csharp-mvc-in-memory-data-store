using exercise.wwwapi.Model.Models;
using exercise.wwwapi.Model.Data;


namespace exercise.wwwapi.Controller.Repository
{
    public class ProductRepository : IProductRepository
    {
        public List<Product> GetAllProducts()
        {
            return ProductCollection.GetProducts();
        }
    }
}
