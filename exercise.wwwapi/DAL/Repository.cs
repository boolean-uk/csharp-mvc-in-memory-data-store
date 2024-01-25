using exercise.wwwapi.Model;
using System.Diagnostics;

namespace exercise.wwwapi.DAL
{
    public class Repository : IRepository
    {
        private readonly DataContext _db;

        public Repository(DataContext db) 
        {
            this._db = db;
        }

        public Product Add(Product product) 
        {
            try 
            {
                Product? productNameAlreadyExists = this._db.Products.FirstOrDefault(x => x.Name == product.Name);
                if (productNameAlreadyExists == null)
                {
                    this._db.Products.Add(product);
                    _db.SaveChanges();
                    Debug.WriteLine("AM: " + this._db.Products.Count());
                    Debug.WriteLine("ID: " + this._db.Products.First().Id);
                    return product;
                }
                throw new ArgumentException($"product with name: {product.Name} already exists");
            }
            catch (Exception ex) 
            {
                Debug.WriteLine(ex);
                throw new ArgumentException($"Product: {product} could not be added to product table in database");
            }
        }

        public List<Product> GetProducts(string? category)
        {
            try
            {
                if (category == null)
                {
                    return this._db.Products.ToList();
                }
                else
                {
                    return this._db.Products.Where(x => x.Category == category).ToList();
                }
            }
            catch(Exception e) 
            {
                Debug.WriteLine(e);
                throw new Exception("Error occured at method: GetProucts(string category) in repostiory");
            }            
        } 

        public Product Get(int id) 
        {
            try 
            {
                Product? product = this._db.Products.FirstOrDefault(x => x.Id == id);
                if (product != null) { return product; }
                throw new IndexOutOfRangeException();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw new Exception("Error occured at method: Get(in tid) in repository");
            }
        }

        public Product Update(Product product, int id) 
        {
            try
            {
                Product? item = this._db.Products.FirstOrDefault(x => x.Id == id);
                if (item != null) 
                {
                    item.Name = product.Name;
                    item.price = product.price;
                    item.Category = product.Category;
                    this._db.SaveChanges();
                    return item;
                }
                throw new IndexOutOfRangeException();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw new Exception("Error occured at method: Update(Product product, int id) in repository");
            }
        }

        public async Task<Product> Delete(int id) 
        {
            try
            {
                Product? item = this._db.Products.FirstOrDefault(x => x.Id == id);
                if (item != null)
                {
                    this._db.Remove(item);
                    await this._db.SaveChangesAsync();
                    return item;
                }
                throw new IndexOutOfRangeException();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw new Exception("Error occured at method: Delete(int id) in repository");
            }
        }
    }
}
