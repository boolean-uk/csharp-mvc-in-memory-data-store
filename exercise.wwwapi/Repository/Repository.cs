using exercise.wwwapi.Data;
using exercise.wwwapi.Model;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private ProductContext _context;
        private DbSet<T> _dbSet;

        public Repository(ProductContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public bool NameExists(string name)
        {
            return _context.Products.Any(p => p.Name == name);
        }

        public T Create(T product)
        {
            _dbSet.Add(product);
            _context.SaveChanges();
            return product;
        }

        public T? Delete(int id)
        {
            T? internalProduct = _dbSet.Find(id);

            if (internalProduct == null)
                return null;

            _dbSet.Remove(internalProduct);
            _context.SaveChanges();

            return internalProduct;
        }

        public IEnumerable<T>? Get()
        {
            if (!_context.Products.Any())
                return null;

            return _dbSet;
        }

        public T? Get(int id)
        {
            return _dbSet.Find(id);
        }

        public T? Update(int id, T product)
        {
            _context.SaveChanges();
            
            return product;
        }
    }
}
