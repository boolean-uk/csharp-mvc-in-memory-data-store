using exercise.wwwapi.Models;
namespace exercise.wwwapi.Data

{
    public class ProductCollection
    {
        private List<Product> products = new List<Product>();

        public Product AddProduct(string name, string category, int price)
        {
            Product product = new Product() { Name = name, Category = category, Price = price };
            product.Id = products.Count + 1;

            return product;
        }

        public Product? getProductByID(int id)
        {
            return products.FirstOrDefault(p => p.Id == id);
        }
    }
}
