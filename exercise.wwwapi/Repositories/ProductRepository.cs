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

        public Product Get(string Id)
        {
            return _db.Products.FirstOrDefault(x => x.Id.ToString() == Id);
        }

        public List<Product> GetAll()
        {
            return _db.Products.ToList();
        }

        public Product Update(string Id, Product entity)
        {
            var entityExists = _db.Products.FirstOrDefault(x => x.Id.ToString() == Id);
            if (entityExists is null)
            {
                return null;
            }

            entity.Id = new Guid(Id);

            _db.Products.Attach(entity);
            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
            return entity;
        }
    }
}
