using mvc_in_memory_data_store.Models;

namespace mvc_in_memory_data_store.Data
{
    public class ProductRepository : IProductRepository
    {
        private static int IdCounter = 1;
        private static List<Product> _products = new List<Product>();

        public Product Add(Product product)
        {
            product.Id = IdCounter++;
            _products.Add(product);
            return product;
        }
        public List<Product> FindAll()
        {

            return _products;
        }

        public Product FindById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public Product Update(int id, Product product)
        {
            var existingProduct = FindById(id);
            if (existingProduct == null)
                return null;

            existingProduct.Name = product.Name;
            existingProduct.Category = product.Category;
            existingProduct.Price = product.Price;

            return existingProduct;
        }

        public Product Delete(int id)
        {
            var product = FindById(id);
            if (product == null)
                return null;

            _products.Remove(product);
            return product;
        }
        public List<Product> FindByCategory(string category)
        {
            return _products.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        public bool ProductNameExists(string name)
        {
            return _products.Any(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
    }

}
