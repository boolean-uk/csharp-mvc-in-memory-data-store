using exercise.wwwapi.Data;
using exercise.wwwapi.Model;

namespace exercise.wwwapi.Repository;

public class Repository : IRepository
{
    private DataContext _db;

    public Repository(DataContext db)
    {
        _db = db;
    }

    public Product AddProduct(PostProduct postProduct)
    {
        var newProduct = new Product() { Name = postProduct.Name, Category = postProduct.Category, Price = int.Parse(postProduct.Price) };
        _db.Products.Add(newProduct);
        _db.SaveChanges();
        return newProduct;
    }

    public IEnumerable<Product> GetProducts()
    {
        return _db.Products.ToList();
    }

    public IEnumerable<Product> GetProducts(string category)
    {
        return _db.Products.Where(x => x.Category == category).ToList();
    }

    public Product? GetProduct(int id)
    {
        var product = _db.Products.FirstOrDefault(x => x.Id == id);
        if (product == null)
        {
            return null;
        }
        return product;
    }

    public Product? UpdateProduct(int id, PutProduct putProduct)
    {
        var toUpdate = _db.Products.FirstOrDefault(x => x.Id == id);
        if (toUpdate == null)
        {
            return null;
        }
        toUpdate.Name = putProduct.Name;
        toUpdate.Category = putProduct.Category;
        toUpdate.Price = putProduct.Price;
        _db.SaveChanges();
        return toUpdate;
    }

    public Product? DeleteProduct(int id)
    {
        var toDelete = _db.Products.FirstOrDefault(x => x.Id == id);
        if (toDelete == null)
        {
            return null;
        }
        _db.Products.Remove(toDelete);
        _db.SaveChanges();
        return toDelete;
    }
}
