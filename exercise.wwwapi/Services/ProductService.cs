using exercise.wwwapi.Data;
using exercise.wwwapi.Model;

namespace exercise.wwwapi.Services
{
    public class ProductService
    {
        private readonly ProductContext _context;
        public ProductService(ProductContext productContext)
        {
            _context = productContext;
        }

        public InternalProduct? CreateInternalProduct(Product product)
        {
            InternalProduct internalProduct = new InternalProduct(product.Name, product.Category, product.Price);

            _context.Products.Add(internalProduct);

            _context.SaveChanges();

            return internalProduct;
        }

        public InternalProduct? UpdateInternalProduct(int id, Product product)
        {
            var internalProduct = _context.Products.Find(id);

            if (internalProduct == null)
                return null;

            internalProduct.Name = product.Name;
            internalProduct.Category = product.Category;
            internalProduct.Price = product.Price;

            _context.SaveChanges();

            return internalProduct;
        }
    }
}