using exercise.wwwapi.Data;
using exercise.wwwapi.Models;
using exercise.wwwapi.Repository.Interfaces;

namespace exercise.wwwapi.Repository
{
    class ProductRepository : IRepository
    {
        private ProductContext _db;

        public ProductRepository(ProductContext db)
        {
            _db = db;
        }

        public ProductItem? Add(ProductPayload payload)
        {
            var product = new ProductItem() { Name = payload.Name, Price = payload.Price, Category = payload.Category };

            if (ProductAlreadyExisits(payload))
            {
                return null;
            }

            _db.Add(product);
            _db.SaveChanges();
            return product;
        }
        public ProductItem? Get(int id)
        {
            return _db.ProductItems.FirstOrDefault(p => p.Id.Equals(id));
        }

        public List<ProductItem> GetAll()
        {
            return _db.ProductItems.ToList();
        }

        public List<ProductItem> GetAll(string category)
        {
            return _db.ProductItems.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public ProductItem? Update(int id, ProductPayload updatePayload)
        {
            if (ProductAlreadyExisits(updatePayload))
            {
                return null;
            }

            var product = Get(id);

            if (!string.IsNullOrWhiteSpace(updatePayload.Name)) { product.Name = updatePayload.Name; }
            if (!(updatePayload.Price < 0)) { product.Price = updatePayload.Price; }
            if (!string.IsNullOrWhiteSpace(updatePayload.Category)) { product.Category = updatePayload.Category; }

            _db.SaveChanges();

            return product;

        }
        public ProductItem? Delete(int id)
        {
            var product = Get(id);
            if (product == null) { return null; }

            _db.ProductItems.Remove(product);
            _db.SaveChanges();
            return product;

        }

        private bool ProductAlreadyExisits(ProductPayload payload)
        {
            if (_db.ProductItems.FirstOrDefault(x => x.Name == payload.Name) == default)
            {
                return false;
            }
            return true;
        }
    }
}
