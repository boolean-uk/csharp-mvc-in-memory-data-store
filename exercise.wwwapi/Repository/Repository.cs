
using exercise.wwwapi.Data;
using exercise.wwwapi.Models;


namespace exercise.wwwapi.Repository
{
    public class Repository<T> : IRepository<T> where T : Products     //Can use IProducts etc.... but for now
    {
        private DataContext<T> _db;
        public Repository(DataContext<T> db)
        {
            _db = db;
        }
        public T Add(T newT)
        {
            _db.products.Add(newT);
            _db.SaveChanges();
            return newT;
        }

        public T Delete(int id)
        {
            var product = _db.products.FirstOrDefault(x => x.id == id);
            if (product == null) { return null; }
            else
            {
                _db.products.Remove(product);
                _db.SaveChanges();
                return product;
            }
        }

        public T GetById(int id)
        {
            var product = _db.products.FirstOrDefault(x => x.id == id);
            return product;
        }

        public List<T> GetAll()
        {
            return _db.products.ToList();
        }

        public T Update(T newT, int id)
        {
            var product = _db.products.FirstOrDefault(x => x.id == id);
            if (product == null) { return null; }
            else
            {
                product.name = newT.name;
                product.price = newT.price;
                product.category = newT.category;
                _db.SaveChanges();
                return product;
            }
        }

        public List<T> Get(string inputCategory)
        {
            var _products = _db.products.Where(product => product.category == inputCategory).ToList();
            if (_products.Count == 0) return null;
            else return _products;
        }

    }
}
