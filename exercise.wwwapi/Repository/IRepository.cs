using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public interface IRepository
    {
        Task<IEnumerable<Products>> GetProducts();
        Task<IEnumerable<Products>> GetProducts(string category);

        Task<Products> GetProduct(int id);
        Task<bool> DeleteProduct(int id);
        Task<Products> AddProduct(Products product);
        Task<Products> UpdateProduct(Products product);

    }
}
