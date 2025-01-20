using System;
using System.Runtime.InteropServices;
using exercise.wwwapi.Data;
using exercise.wwwapi.Models;
using exercise.wwwapi.ModelViews;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Repository;

public class ProductRepository : IProductRepository
{

    private DataContext _db;

    public ProductRepository(DataContext db)
    {
        _db = db;
    }
    public async Task<ProductResponse> CreateProduct(ProductPost prodPost)
    {
        Product product = new Product {Name = prodPost.Name, Category = prodPost.Category, Price = prodPost.Price};

        if (_db.Product.Any(prod => prod.Name == product.Name))
        {
            return new ProductResponse("Conflict");
        }
        
        await _db.Product.AddAsync(product);
        await _db.SaveChangesAsync();
        ProductResponse response = new ProductResponse(product);
        response.Status = "Created";
        return response;
        
    }

    public async Task<ProductResponse> DeleteProduct(int id)
    {
        Product prod = await _db.Product.FindAsync(id);
        if (prod == null)
        {
            return new ProductResponse("Not Found");
        }

        _db.Product.Remove(prod);
        await _db.SaveChangesAsync();
        return new ProductResponse(prod);
    }

    public async Task<ProductResponse> GetProduct(int id)
    {
        Product prod = await _db.Product.FindAsync(id);
        if (prod == null)
        {
            return new ProductResponse("Not Found");
        }
        ProductResponse response = new ProductResponse(prod);
        response.Status = "Get";
        return response;
        
    }

    public async Task<IEnumerable<ProductResponse>> GetProducts(string category)
    {   
        List<Product> products;
        List<ProductResponse> response = new List<ProductResponse>();
        if (category == "")
        {
            //Get all products no matter category
            products = _db.Product.ToList();
            response = new List<ProductResponse>();
            foreach (Product p in products)
            {
                response.Add(new ProductResponse(p));
            }
        }

        else 
        {
            // Get all products in the given category
            products = _db.Product.Where(prod => prod.Category == category).ToList();

        if (products.Count > 0)
        {
            // Products in category found
            foreach (Product p in products)
            {
                response.Add(new ProductResponse(p));
            }
            
        }
        else
        {
            //No products in given category found
            response.Add(new ProductResponse("Not Found"));
        }

        }
        return response;
        
        
    }

    public async Task<ProductResponse> UpdateProduct(int id, ProductPut prodPut)
    {
        ProductResponse response;
        Product prod = await _db.Product.FirstOrDefaultAsync(prod => prod.Id == id);
        if (prod == null)
        {
            response = new ProductResponse("Not Found");
            return response;
        }

        else if (_db.Product.Any(prod => prod.Name == prodPut.Name))
        {
            response = new ProductResponse("Conflict");
        }

        if (prodPut.Name != null && prodPut.Name != "")
        {
            prod.Name = prodPut.Name;
        }
        if (prodPut.Category != null && prodPut.Category != "")
        {
            prod.Category = prodPut.Category;
        }
        if (prodPut.Price != null)
        {
            prod.Price = (decimal) prodPut.Price;
        }

        await _db.SaveChangesAsync();
        response = new ProductResponse(prod);
        response.Status = "Updated";
        return response;
    }
}
