using exercise.wwwapi.Data;
using exercise.wwwapi.Model;

namespace exercise.wwwapi.Repositories
{
    public class Repository
    {
        private ProductContext _productContext;

        public Repository(ProductContext productContext)
        {
            _productContext = productContext;
        }

        // --Product--
        public Product GetProduct(uint id)
        {
            Product product = _productContext.Objects.FirstOrDefault(x => x.ID == id);
            return product;
        }
        public IEnumerable<Product> GetProducts()
        {
            return _productContext.Objects.ToList();
        }
        public Product CreateProduct(Product product)
        {
            uint lastID = (_productContext.Objects.Count() > 0) ? (_productContext.Objects.Last().ID + 1) : 0;

            product.ID = lastID;
            _productContext.Objects.Add(product);
            
            _productContext.SaveChanges();
            return product;
        }
        public Product UpdateProduct(uint id, Product newProduct)
        {
            Product product = _productContext.Objects.FirstOrDefault(_x => _x.ID == id);
            if(product == null || product == default(Product))
                return null;

            product.Price = newProduct.Price;
            product.Category = newProduct.Category;
            product.Name = newProduct.Name;

            _productContext.SaveChanges();
            return product;
        }

       public void DeleteProduct(uint productID)
       {
            _productContext.Objects.Remove(_productContext.Objects.FirstOrDefault(x => x.ID == productID));
            _productContext.SaveChanges();
            return;
       }
    }
}