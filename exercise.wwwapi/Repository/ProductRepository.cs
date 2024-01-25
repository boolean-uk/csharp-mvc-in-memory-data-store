using Microsoft.AspNetCore.Authentication;
using exercise.wwwapi.Data;
using exercise.wwwapi.Model;
using System.ComponentModel;

namespace exercise.wwwapi.Repository
{
    public class ProductRepository
    {
        private ProductsContext _context;
        public ProductRepository(ProductsContext context)
        {
            _context = context;
        }

        public List<Products> GetAllProducts(string category)
        {
            List<Products> productsCategory = new List<Products>();
            foreach (var product in _context.Products)
            {
                if(product.category == category) productsCategory.Add(product);
            }
            return productsCategory.Count > 0 ? productsCategory : null;
        }

        public Products? CreateProduct(string name, string categroy, int price)
        {
            var Product = new Products() { name = name, category = categroy, price=price};
            _context.Add(Product);
            _context.SaveChanges();
            return Product;
        }

        public bool ProductExists(string name)
        {
            return _context.Products.Any(p => p.name == name);
        }
       

        public Products? GetProduct(int id)
        {
            return _context.Products.FirstOrDefault(p => p.Id == id);
        }

        public Products? UpdateProduct(int id, string? newname, string? newcategory, int? newprice)
        {
            // check if task exists
            var product = GetProduct(id);
            if (product == null)
            {
                return null;
            }

            bool hasUpdate = false;

            if (newname != null)
            {
                product.name = (string)newname;
                hasUpdate = true;
            }

            if (newcategory != null)
            {
                product.category = newcategory;
                hasUpdate = true;
            }

            if (!hasUpdate) throw new Exception("No task update data provided");

            _context.SaveChanges();

            return product;
        }

        public bool DeleteProduct(int id)
        {
            var product = GetProduct(id);
            if (product == null) return false;
            _context.Products.Remove(product);
            _context.SaveChanges();
            return true;
        }



    }
}



     

       
