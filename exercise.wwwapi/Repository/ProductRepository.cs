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

        public InternalProduct Create(Product product)
        {
            InternalProduct internalProduct = new InternalProduct(product.Name, product.Category, product.Price);
            _db.Products.Add(internalProduct);
            return internalProduct;
        }

        public InternalProduct Delete(int id)
        {
            InternalProduct internalProduct = _db.Products.FirstOrDefault(x => x.Id == id);

            _db.Products.Remove(internalProduct);

            return internalProduct;
        }

        public IEnumerable<InternalProduct> Get()
        {
            return _db.Products;
        }

        public InternalProduct Get(int id)
        {
            return _db.Products.FirstOrDefault(x => x.Id == id);
        }

        public InternalProduct Update(int id, Product product)
        {
            InternalProduct internalProduct = _db.Products.FirstOrDefault(x => x.Id == id);

            internalProduct.Name = product.Name;
            internalProduct.Category = product.Category;
            internalProduct.Price = product.Price;

            return internalProduct;
        }
    }
}
