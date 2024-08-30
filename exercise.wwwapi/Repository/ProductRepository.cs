using exercise.wwwapi.Data;
using exercise.wwwapi.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace exercise.wwwapi.Repository
{
    public class ProductRepository : IRepository<Product>
    {
        public static int Id = 1;

        DataContext _db;

        public ProductRepository(DataContext db)
        {
            _db = db;
        }


        public Product Add(Product product)
        {
            product.Id = Id;
            _db.Add(product);
            _db.SaveChanges();
            Id++;
            return product; 
        }


        public List<Product> GetAll(string? category)
        {
            List<Product> products = _db.Products.Where(p => p.Category == category).ToList();

            return products == null ? _db.Products.ToList() : products;
        }


        public Product Get(int id)
        {
            var product = _db.Products.FirstOrDefault(p => p.Id == id);
            return product;
        }


        public Product Update(int id, Product entity)
        {
            var product = _db.Products.FirstOrDefault(p => p.Id == id);

            if (product == null) return null; // doesn't exist

            product.Name = entity.Name;
            product.Category = entity.Category;
            product.Price = entity.Price;
            _db.SaveChanges();
            return product;
        }


        public Product Delete(int id)
        {
            var product = Get(id);

            if (product == null) return null; // doesnt exist

            _db.Products.Remove(product);
            _db.SaveChanges();
            return product;
        }

        public Product GetByName(string name)
        {
            var product = _db.Products.FirstOrDefault(p => p.Name == name);
            return product;
        }
    }
}
