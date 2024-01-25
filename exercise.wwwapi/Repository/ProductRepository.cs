using exercise.wwwapi.Data;
using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public class ProductRepository : IRepository
    {
        private ProductContext _db;
        public ProductRepository(ProductContext db)
        {
            _db = db;
        }

        /// <inheritdoc/>
        public Product? DeleteProduct(int id)
        {
            Product? prod = _db.Products.FirstOrDefault(p => p.Id == id);
            if (prod != null)
            {
                _db.Products.Remove(prod);
            }
            _db.SaveChanges();
            return prod;
        }

        /// <inheritdoc/>
        public IEnumerable<Product> GetProducts(string? category)
        {
            IEnumerable<Product> prods = new List<Product>();
            if (category == null)
            { // Get everything
                prods = _db.Products.ToList();
            }
            else 
            { // Get everything within a specified category
                prods = _db.Products.Where(p => p.Category.ToLower() == category.ToLower()).ToList();
            }
            return prods;
        }

        /// <inheritdoc/>
        public Product? GetSpecificProduct(int id)
        {
            Product? prod = _db.Products.FirstOrDefault(p => p.Id == id);
            return prod;
        }

        /// <inheritdoc/>
        public Product? PostProduct(ProductPost prod)
        {
            if ((prod.Name == null || !(ProductNameIsAvailable(prod.Name))) || (prod.Category == null) || (prod.Price == null)) 
            {
                return null;
            }

            Product newProd = new Product(prod.Name, prod.Category, prod.Price);
            _db.Products.Add(newProd);
            _db.SaveChanges();
            return newProd;
        }

        /// <inheritdoc/>
        public bool ProductNameIsAvailable(string? name)
        {
            return !_db.Products.Any(p => p.Name == name);
        }

        /// <inheritdoc/>
        public Tuple<Product?, int> PutProduct(int id, ProductPut prod)
        {
            Product? orgProduct = _db.Products.FirstOrDefault(p => p.Id == id);
            if (orgProduct == null)
            {
                return new Tuple<Product?, int>(orgProduct, 404);
            }
            if (!(ProductNameIsAvailable(prod.Name))) 
            {
                return new Tuple<Product?, int>(orgProduct, 400);
            }

            orgProduct.Name = prod.Name ?? orgProduct.Name;
            orgProduct.Category = prod.Category ?? orgProduct.Category;
            orgProduct.Price = prod.Price;

            _db.SaveChanges();
            return new Tuple<Product?, int>(orgProduct, 201);
        }
    }
}
