using exercise.wwwapi.Data;
using exercise.wwwapi.Model;

namespace exercise.wwwapi.Repository
{
    public class Repository : IRepository
    {
        //private static List<Product> _products = new List<Product>();
        private DataContext _db;
        public Repository(DataContext dataContext)
        {
            _db = dataContext;
        }




        public Product AddProduct(Product product)
        {
            if (_db.products.FirstOrDefault(p => p.Name == product.Name) != null)
            {
                return null;
            }

            _db.products.Add(product);
            _db.SaveChanges();
            return product;

        }

        public bool DeleteProduct(int id)
        {
           throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAllProducts(string category)
        {
            return _db.products.Where(p => p.Category == category).ToList();
           // return _db.products.ToList();
        }

        public Product? GetProductByID(int id)
        {
           
            var product = _db.products.FirstOrDefault(x => x.Id == id);
            return product;
        }



        public Product? UpdateProduct(int id, ProductPut product)
        {
            /**var updatedProduct = _db.products.Where(p => p.Id == id).FirstOrDefault();
            if (updatedProduct == null)
            {
                return false;
            }
            

            updatedProduct.Name = !string.IsNullOrEmpty(newProduct.Name) ? newProduct.Name : updatedProduct.Name;
            updatedProduct.Category = !string.IsNullOrEmpty(newProduct.Category) ? newProduct.Category : updatedProduct.Category;
            updatedProduct.Price = (decimal)((newProduct.Price != 0) ? newProduct.Price : updatedProduct.Price);
            _db.SaveChanges();
            return true;*/
            var newProduct = _db.products.FirstOrDefault(p => p.Id == id);

            if (newProduct == null)
            {
                return null;
            }

            if (_db.products.FirstOrDefault(p => p.Name == product.Name) != null)
            {
                newProduct.Name = null;
                return newProduct;
            }

            newProduct.Name = product.Name;
            newProduct.Category = product.Category.ToLower();
            newProduct.Price = int.Parse(product.Price);
            _db.SaveChanges();
            return newProduct;
        }

        Product? IRepository.DeleteProduct(int id)
        {
            /* var product = _db.products.Where(p => p.Id == id).FirstOrDefault();
             if (product != null)
             {
                 _db.products.Remove(product);
                 _db.SaveChanges();
                 return true;
             }
             return false;
            */
            var deletedProduct = _db.products.FirstOrDefault(product => product.Id == id);

            if (deletedProduct == null)
            {
                return null;
            }

            _db.Remove(deletedProduct);
            _db.SaveChanges();
            return deletedProduct;
        }
        public IEnumerable<Product> GetAllProducts()
        {
            return _db.products.ToList();
        }

    }
}
