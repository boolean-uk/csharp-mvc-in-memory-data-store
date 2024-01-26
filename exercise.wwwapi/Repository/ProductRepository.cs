using exercise.wwwapi.Data;
using exercise.wwwapi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace exercise.wwwapi.Repository
{
    public class ProductRepository<T> : IRepository<T> where T : class
    {
        private DataContext _db;
        private DbSet<T> _table = null;

        public ProductRepository(DataContext db)
        {
            _db = db;
            _table = _db.Set<T>();
        }

        /// <inheritdoc/>
        public Product? DeleteProduct(int id)
        {
            Product? prod = _db.Products.FirstOrDefault(p => p.Id == id);
            if (prod != null)
            {
                _db.Products.Remove(prod);
            }
            _db.SaveChanges();
            return prod;
        }


        /// <inheritdoc/>
        public IEnumerable<Product> GetProducts(string? category)
        {
            IEnumerable<Product> prods = new List<Product>();
            if (category == null)
            { // Get everything
                prods = _db.Products.ToList();
            }
            else 
            { // Get everything within a specified category
                prods = _db.Products.Where(p => p.Category.ToLower() == category.ToLower()).ToList();
            }
            return prods;
        }

        /// <inheritdoc/>
        public Product? GetSpecificProduct(int id)
        {
            Product? prod = _db.Products.FirstOrDefault(p => p.Id == id);
            return prod;
        }



        /// <inheritdoc/>
        public Product? PostProduct(ProductPost prod)
        {
            if ((prod.Name == null || !(ProductNameIsAvailable(prod.Name))) || (prod.Category == null) || (prod.Price == null)) 
            {
                return null;
            }

            Product newProd = new Product(prod.Name, prod.Category, prod.Price);
            _db.Products.Add(newProd);
            _db.SaveChanges();
            return newProd;
        }

        /// <inheritdoc/>
        public bool ProductNameIsAvailable(string? name)
        {
            return !_db.Products.Any(p => p.Name == name);
        }

        /// <inheritdoc/>
        public Tuple<Product?, int> PutProduct(int id, ProductPut prod)
        {
            Product? orgProduct = _db.Products.FirstOrDefault(p => p.Id == id);
            if (orgProduct == null)
            {
                return new Tuple<Product?, int>(orgProduct, 404);
            }
            if (!(ProductNameIsAvailable(prod.Name))) 
            {
                return new Tuple<Product?, int>(orgProduct, 400);
            }

            orgProduct.Name = prod.Name ?? orgProduct.Name;
            orgProduct.Category = prod.Category ?? orgProduct.Category;
            orgProduct.Price = prod.Price;

            _db.SaveChanges();
            return new Tuple<Product?, int>(orgProduct, 201);
        }
        public T Insert(T entity)
        {

            _table.Add(entity);
            _db.SaveChanges();
            return entity;

        }

        public Tuple<T, int> Update(int id, T entity)
        {
            throw new NotImplementedException();
        }
        public T? GetById(int id)
        {
            return _table.Find(id);
        }

        public IEnumerable<T> Get(string? category)
        {
            T instance = 

            if (category == null)
            {
                return _table.ToList();
            }
            if (typeof(T) is Product)
            {

            }
            else 
            {
                return _table.Where(e => e.Category).ToList();
            }
            
        }

        public bool NameIsAvailable(string? name)
        {
            if (name == null) { return true; }
            if ( _table.Where(e => e.) ) { }
        }

        public T Delete(int id)
        {
            T entity = _table.Find(id);
            _table.Remove(entity);
            _db.SaveChanges();

            return entity;
        }
    }
}
