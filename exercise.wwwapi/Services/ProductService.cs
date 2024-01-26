using exercise.wwwapi.Data;
using exercise.wwwapi.Model;
using exercise.wwwapi.Repository;

namespace exercise.wwwapi.Services
{
    public class ProductService
    {
        private readonly ProductContext _context;
        public ProductService(ProductContext productContext)
        {
            _context = productContext;
        }

        public InternalProduct? CreateInternalProduct(IRepository<InternalProduct> repository, Product product)
        {
            InternalProduct internalProduct = new InternalProduct(product.Name, product.Category, product.Price);

            return repository.Create(internalProduct);
        }

        public InternalProduct? UpdateInternalProduct(IRepository<InternalProduct> repository, int id, Product product)
        {
            var internalProduct = _context.Products.Find(id);

            if (internalProduct == null)
                return null;

            internalProduct.Name = product.Name;
            internalProduct.Category = product.Category;
            internalProduct.Price = product.Price;

            return repository.Update(id, internalProduct);
        }
    }
}