using exercise.wwwapi.Models.Data;

namespace exercise.wwwapi.Models
{
    public static class ProductCollection
    {
        private static List<Product> _products = new List<Product>()
        {
            new Product(1, "Makeup", "Beauty", 20),
            new Product(2, "Hair conditioner", "Beauty", 32),
            new Product(3, "Pasta", "Food", 20),
            new Product(4, "Meatballs", "Food", 10),
            new Product(5, "Taco shells", "Food", 60),
        };

        public static List<Product> GetProducts() { return _products; }
        public static Product GetProduct(int id) { return _products.FirstOrDefault(x=>x.Id==id); }
        public static Product AddProduct(Product product)
        {
            if (_products.Find(x=>x.Name==product.Name) == null)
            {
                _products.Add(product);
                return product;
            }
            return null;
        }

        public static Product UpdateProduct(int id, Product product)
        {
            Product productHere = null;
            if (_products.Find(x => x.Id == id) != null)
            {
                productHere.Name = product.Name;
                productHere.Category = product.Category;
                productHere.Price = product.Price;
                return productHere;
            } else
            return productHere;
        }

        public static Product DeleteProduct(int id)
        {
            Product productHere = _products.FirstOrDefault(x => x.Id == id);
            if (_products.Remove(productHere))
            {
                return productHere;
            }
            return null;
        }
    }
}
