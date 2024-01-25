using exercise.wwwapi.Data;
using exercise.wwwapi.Model;

namespace exercise.wwwapi.Repository
{
    public class ProductRepository : IProductRepository
    {
        private ProductContex _db;

        public ProductRepository(ProductContex db)
        {
            _db = db;
        }

        public bool NameExists(string name)
        {
            return _db.Products.Any(p => p.Name == name);
        }

        public InternalProduct Create(Product product)
        {
            InternalProduct internalProduct = new InternalProduct(product.Name, product.Category, product.Price);
            _db.Products.Add(internalProduct);
            _db.SaveChanges();
            return internalProduct;
        }


        public InternalProduct? Delete(int id)
        {
            InternalProduct? internalProduct = _db.Products.FirstOrDefault(x => x.Id == id);

            if (internalProduct == null)
                return null;

            _db.Products.Remove(internalProduct);
            _db.SaveChanges();

            return internalProduct;
        }

        public IEnumerable<InternalProduct>? Get()
        {
            if (!_db.Products.Any())
                return null;

            return _db.Products;
        }

        public InternalProduct? Get(int id)
        {
            return _db.Products.FirstOrDefault(x => x.Id == id);
        }

        public InternalProduct? Update(int id, Product product)
        {
            InternalProduct internalProduct = _db.Products.FirstOrDefault(x => x.Id == id);

            if (internalProduct == null)
                return null;

            internalProduct.Name = product.Name;
            internalProduct.Category = product.Category;
            internalProduct.Price = product.Price;
            _db.SaveChanges();
            
            return internalProduct;
        }
    }
}
