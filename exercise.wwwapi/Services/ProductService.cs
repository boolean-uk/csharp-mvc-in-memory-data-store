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

        public InternalProduct CreateInternalProduct(Product product)
        {
            InternalProduct internalProduct = new InternalProduct(product.Name, product.Category, product.Price);

            return internalProduct;
        }

        public InternalProduct? UpdateInternalProduct(int id, Product product)
        {
            InternalProduct internalProduct = _context.Products.Find(id);

            if (internalProduct == null)
                return null;

            internalProduct.Name = product.Name;
            internalProduct.Category = product.Category;
            internalProduct.Price = product.Price;

            return internalProduct;
        }
    }
}