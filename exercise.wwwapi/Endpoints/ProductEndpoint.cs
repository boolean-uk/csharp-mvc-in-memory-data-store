
using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;
using Microsoft.AspNetCore.Builder;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoint
    {
        public static void ConfigureProductEndpoint(this WebApplication app)
        {
            var productGroup = app.MapGroup("products");

            productGroup.MapPost("/", CreateProduct);
            productGroup.MapGet("/", AllProducts);
            productGroup.MapGet("/{id}", Product);
            productGroup.MapPut("/{id}", UpdateProduct);
            productGroup.MapDelete("/{id}", DeleteProduct);
        }

        private static async Task<IResult> CreateProduct(IRepository<Product> repository, ProductBody productBody)
        {
            Product product = new Product()
            {
                Id = repository.Get().Max(b => b.Id) + 1,
                Name = productBody.Name,
                Price = productBody.Price,
                Category = productBody.Category
            };
            repository.Insert(product);
            return TypedResults.Created(product.Id.ToString(), product);
        }
        private static async Task<IResult> AllProducts(IRepository<Product> repository)
        {
            return TypedResults.Ok(repository.Get());
        }
        private static async Task<IResult> Product(IRepository<Product> repository, int id)
        {
            Product product = repository.GetById(id);

            return TypedResults.Ok(product);
        }
        private static async Task<IResult> UpdateProduct(IRepository<Product> repository, int id, ProductBody productBody)
        {
            Product product = new Product();
            if (productBody != null)
            {
                product.Id = id;
                product.Name = productBody.Name;
                product.Price = productBody.Price;
                product.Category = productBody.Category;
            }
            
            return TypedResults.Ok(repository.Update(product));
        }
        private static async Task<IResult> DeleteProduct(IRepository<Product> repository, int id)
        {
            Product product = repository.GetById(id);
            repository.Delete(id);
            return TypedResults.Ok(product);
        }
    }
}
