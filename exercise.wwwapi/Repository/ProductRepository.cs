using exercise.wwwapi.Data;
using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public class ProductRepository : IProductRepository
    {
        //private ProductCollection _products;
        private ProductContext _context;
        private int _id = 0;
        public ProductRepository(ProductContext context)
        {
            _context = context;
        }

        public Product CreateAProduct(ProductPostPayload productPost)
        {
            _id++;
            Product product = new Product() { name = productPost.name, price = productPost.price, category = productPost.category, Id = _id };
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }

        public bool DeleteAProduct(int id)
        {
            var product = GetAProduct(id);
            if (product == null) return false;
            _context.Products.Remove(product);  
            _context.SaveChanges();
            return true;
        }

        public Product? GetAProduct(int id)
        {
            
            return _context.Products.FirstOrDefault(p => p.Id == id);   
        }

        public List<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public Product? UpdateAProduct(int id, ProductUpdateData updateData)
        {
            var product = GetAProduct(id);
            if (product == null)
            {
                return null;
            }

            bool wasUpdated = false;
            
            if(updateData.name != null) 
            {
                product.name = updateData.name;
                wasUpdated = true;
            }
            if (updateData.price != product.price)
            {
                product.price = updateData.price;
                wasUpdated = true;
            }
            if (updateData.category != null)
            {
                product.category = updateData.category;
                wasUpdated = true;
            }

            if (!wasUpdated) throw new Exception("No product update data provided");

            _context.SaveChanges();

            return product;
        }
    }
}
