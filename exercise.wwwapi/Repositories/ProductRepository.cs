using exercise.wwwapi.Data;
using exercise.wwwapi.Models;
using System.Runtime.CompilerServices;

namespace exercise.wwwapi.Repositories
{
    public class ProductRepository : IRepository<Product, ProductPostModel>
    {
        private readonly ProductContext _db;
        public ProductRepository(ProductContext db)
        {
            this._db = db;
        }

        public bool Exists(ProductPostModel entity) //Product with this name already exists
        {
            foreach (Product product in this._db.products)
            {
                if (product.name == entity.name)
                {
                    return true;
                }
            }
            return false;
        }

        public Product Create(ProductPostModel entity) //Create a new product and add it to the database
        {
            Product product = new Product() { id = 0, category = entity.category, name = entity.name, price = entity.price };
            this._db.Add(product);
            this._db.SaveChanges();
            return product;
        }

        public Product? Delete(int identifier) //Delete the product with this id
        {
            var product = this._db.products.FirstOrDefault(x => x.id == identifier);
            if (product != null)
            {
                this._db.products.Remove(product);
                this._db.SaveChanges();
            }
            return product;
        }

        public Product? Get(int identifier) //Get the product with this id
        {
            var product = this._db.products.FirstOrDefault(x => x.id == identifier);
            return product;
        }

        public List<Product> GetAll(string category) //Get all products of this category
        {
            List<Product> list = this._db.products.Where(x => x.category == category).ToList();
            return list;
        }

        public Product? Update(ProductPostModel entity, int identifier) //Update a product by altering the information to the new info given
        {
            var product = this._db.products.SingleOrDefault(x => x.id == identifier);
            if (product != null)
            {
                product.category = entity.category;
                product.name = entity.name;
                product.price = entity.price;
                this._db.SaveChanges();
            }
            return product;
        }
    }
}
