using exercise.wwwapi.Data;
using exercise.wwwapi.Models;
using Microsoft.AspNetCore.Components.Forms;
using System.Data.Common;

namespace exercise.wwwapi.Repository
{
    public class Repository:IRepository
    {
        private ProductContext _productDb;

        public Repository(ProductContext productDb)
        {
            _productDb = productDb;

        }

        public IEnumerable<Product> GetAll()
        {
            return _productDb.Products; 
            
        }

        public IEnumerable<Product> GetByCategory(string input)
        {
            List<Product> products = new List<Product>();
            foreach (var item in _productDb.Products)
            {
                if (item.Category.ToLower() == input.ToLower())
                {
                    products.Add(item);
                }
            }
                return products;
        }

        public Product GetById(int id)
        {
            Product singleProduct = _productDb.Products.FirstOrDefault(x => x.Id.Equals(id));
            return singleProduct;
        }
        public Product CreateProduct(ProductPost model)
        {
            int id = 1;
            if (_productDb.Products.Any()) { id=_productDb.Products.Max(x => x.Id) + 1; }
            Product newProduct = new Product() {Id=id,Name= model.Name,Category= model.Category,Price= model.Price };
            
            _productDb.Products.Add(newProduct);
            _productDb.SaveChanges();
            
            return newProduct;
        }

        public Product UpdateProduct(int id, ProductPost model) { 
        
            Product getProduct = _productDb.Products.FirstOrDefault(x => x.Id.Equals(id));
            if (getProduct == null) { getProduct = new Product(); }
            getProduct.Price = model.Price;
            getProduct.Name = model.Name;
            getProduct.Category = model.Category;
            _productDb.SaveChanges();
            return getProduct;
        }

        public Product DeleteProduct(int id)
        {
            Product getProduct = _productDb.Products.FirstOrDefault(x => x.Id.Equals(id));
            if(getProduct != null) { _productDb.Products.Remove(getProduct); }
            _productDb.SaveChanges();
            return getProduct;

        }
    }
}
