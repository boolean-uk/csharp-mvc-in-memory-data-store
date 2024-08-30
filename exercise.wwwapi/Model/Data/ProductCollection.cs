using exercise.wwwapi.Model.Models;

namespace exercise.wwwapi.Model.Data
{
    public static class ProductCollection
    {
        private static List<Product> products = new List<Product> 
        { 
            new Product("Spoon", "Cuttlery", 2)
        };

        public static List<Product> GetProducts() { return products; }

        public static Product GetProductById(int id) { return products[id]; }

        public static Product AddProduct(Product P)
        {
            products.Add(P);
            return P;
        }

        public static Product RemoveProduct(Product P)
        {
            products.Remove(P);
            return P;
        }
    }
}
