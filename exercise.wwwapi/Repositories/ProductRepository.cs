using exercise.wwwapi.Data;
using exercise.wwwapi.Model;
using exercise.wwwapi.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace exercise.wwwapi.Repositories
{
    public class ProductRepository : IRepository
    {

        private readonly DataContext _Db;
       
        public ProductRepository(DataContext db)
        {
            _Db = db;
        }

        public string message { get; set; }

        public Product create(Product product)
        {
            _Db.products.Add(product);
            _Db.SaveChanges();
            return product;
        }

        public Product delete(int id)
        {
            var product = _Db.products.FirstOrDefault(x => x.id == id);
            if (product == null)
            {
                return null;
            }
            _Db.products.Remove(product);
            _Db.SaveChanges();
            return product;
        }

        public Product get(int id)
        {
           var product = _Db.products.FirstOrDefault( x => x.id== id);
            return product;
        }

        public List<Product> getAll()
        {
            return _Db.products.ToList();
        }

        public Product update(int id, ProductViewModel product)
        {
            var productToUpdate = _Db.products.FirstOrDefault(x => x.id == id);
            productToUpdate.name = product.name;
            productToUpdate.price = product.price;


            _Db.Entry(productToUpdate).State= EntityState.Modified;
            _Db.SaveChanges();
            return productToUpdate;
            }

        public bool checkIfExists(string name)
        {
            if(_Db.products.Where(x => x.name == name).Any())
            {
                return true;
            }
            return false;
        }
    }
}
