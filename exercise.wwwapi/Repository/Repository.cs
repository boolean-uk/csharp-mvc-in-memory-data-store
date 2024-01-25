using exercise.wwwapi.Data;
using exercise.wwwapi.Objects;
using Microsoft.AspNetCore.Authentication;

namespace exercise.wwwapi.Repository
{
    public class Repository : IRepository
    {
        private DataContext _db;
        public Repository(DataContext db)
        {
            _db = db;
        }
        public Product CreateProduct(InputProduct NewProduct)
        {
            //Product with same name exists
            if (_db.products.Any(x => x.Name == NewProduct.Name))
            {
                return null;
            }
            var CreatedProd = new Product() 
            { 
                Id = _db.products.Count() == 0 ? 0 : _db.products.Max(x => x.Id)+1,
                Category = NewProduct.Category, 
                Name = NewProduct.Name, 
                Price = int.Parse(NewProduct.Price)
            };
            _db.Add(CreatedProd);
            _db.SaveChanges();
            return CreatedProd;
        }

        public Product DeleteProduct(int id)
        {
            Product? product = GetProductById(id);
            if (product != null)
            {
                _db.Remove(product);
                _db.SaveChanges();
            }
            return product;
        }

        public Product GetProductById(int id)
        {
            return _db.products.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Product> GetProducts(string category)
        {
            return string.IsNullOrEmpty(category) ? _db.products.ToList() : _db.products.Where(x=>x.Category == category).ToList();
        }

        public Product UpdateProduct(InputProduct UpdatedProduct, int id)
        {
            Product? existing = GetProductById(id);
            if (existing != null)
            {
                existing.Name = string.IsNullOrEmpty(UpdatedProduct.Name) ? existing.Name : UpdatedProduct.Name;
                existing.Price = string.IsNullOrEmpty(UpdatedProduct.Price) ? existing.Price : int.Parse(UpdatedProduct.Price);
                existing.Category = string.IsNullOrEmpty(UpdatedProduct.Category) ? existing.Category : UpdatedProduct.Category;
            }
            _db.SaveChanges();
            return existing;
        }
    }
}
