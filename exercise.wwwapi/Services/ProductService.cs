using exercise.wwwapi.Data;
using exercise.wwwapi.Model;

namespace exercise.wwwapi.Services
{
    public class ProductService
    {
        private static ProductContex _context;
        public ProductService(ProductContex productContext) 
        {
            _context = productContext;
        }

        public static InternalProduct ConvertProduct(Product product) 
        {
            return new InternalProduct(product.Name, product.Category, product.Price);
        }

        public static InternalProduct? ConvertProduct(Product product, int id)
        {
            InternalProduct? internalProduct = _context.Products.Find(id);

            if (internalProduct == null)
                return null;

            internalProduct.Name = product.Name;
            internalProduct.Category = product.Category;
            internalProduct.Price = product.Price;

            return internalProduct;
        }
    }
}
