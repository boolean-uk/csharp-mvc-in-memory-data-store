using exercise.wwwapi.Data;
using exercise.wwwapi.Models;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private DataContext _db;
        public ProductRepository(DataContext db)
        {
            _db = db;
        }
        public Product Add(Product entity)
        {
            var entityExists = _db.Products.FirstOrDefault(x => x.Name == entity.Name);
            if (entityExists is not null)
            {
                return null;
            }

            _db.Add(entity);
            _db.SaveChanges();
            return entity;
        }

        public Product Delete(string Id)
        {
            var entityExists = _db.Products.FirstOrDefault(x => x.Id.ToString() == Id);
            if (entityExists is null)
            {
                return null;
            }

            _db.Remove(entityExists);
            _db.SaveChanges();
            return entityExists;
        }

        public Product GetById(string Id)
        {
            return _db.Products.FirstOrDefault(x => x.Id.ToString() == Id);
        }

        public Product GetByName(string name) 
        { 
            return _db.Products.FirstOrDefault(y => y.Name == name); 
        }

        public List<Product> GetAll(string category)
        {
            if (string.IsNullOrEmpty(category))
            {
                return _db.Products.Where(x => x.Category == category).ToList();
            }
            return _db.Products.ToList();
        }

        public Product Update(string Id, Product entity)
        {
            var entityExists = _db.Products.FirstOrDefault(x => x.Id.ToString() == Id);
            if (entityExists is null)
            {
                return null;
            }

            entityExists.Name = entity.Name;
            entityExists.Category = entity.Category;
            entityExists.Price = entity.Price;

            _db.Products.Attach(entityExists);
            _db.Entry(entityExists).State = EntityState.Modified;
            _db.SaveChanges();
            return entityExists;
        }
    }
}
