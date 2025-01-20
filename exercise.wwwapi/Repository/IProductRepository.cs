using System;
using exercise.wwwapi.Models;
using exercise.wwwapi.ModelViews;

namespace exercise.wwwapi.Repository;

public interface IProductRepository
{
    Task<ProductResponse> CreateProduct(ProductPost prodView);
    Task<IEnumerable<ProductResponse>> GetProducts(string category);
    Task<ProductResponse> GetProduct(int id);
    Task<ProductResponse> UpdateProduct(int id, ProductPut prodView);
    Task<ProductResponse> DeleteProduct(int id);


}
