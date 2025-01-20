using System;
using exercise.wwwapi.Model;
using exercise.wwwapi.View;

namespace exercise.wwwapi.Repositories;

public interface IRepository
{
    Task<IEnumerable<Product>> GetProducts();
    Task<Product> GetProduct(Guid id);
    Task<bool> DeleteProduct(Guid id);
    Task<Product> AddProduct(Product product);
    Task<Product> UpdateProduct(Guid id, ProductPut product);
}
