using exercise.wwwapi.Model;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Repository
{
   
    public interface IProductRepository
    {
        public Task<List<Product>> GetAllProducts(string? filter);

       
        public Task<Product> AddProduct(string name, string category, int price);

        public Task<Product?> GetProduct(int id);

        public Task<Product?> UpdateProduct(int id, ProductUpdatePayload updateData);

        public  Task<bool> DeleteProduct(int id);
    }
}
