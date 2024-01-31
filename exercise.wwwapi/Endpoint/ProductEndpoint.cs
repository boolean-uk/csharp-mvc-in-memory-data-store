using exercise.wwwapi.Model;
using exercise.wwwapi.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Runtime.CompilerServices;

namespace exercise.wwwapi.Endpoint
{
    public static class ProductEndpoint
    {
        public static void ConfigureProductEndpoints(this WebApplication app)
        {
            var productGroup = app.MapGroup("Products");

            productGroup.MapPost("/", AddProduct).AddEndpointFilter(async (invocationContext, next) =>
            {
                var product = invocationContext.GetArgument<ProductPost>(1);

                if (string.IsNullOrEmpty(product.name) || string.IsNullOrEmpty(product.category) || product.price == null)
                {
                    return Results.BadRequest("Please fill in all information");
                }
                if(string.Equals(product.name, "string") || string.Equals(product.category, "string")) 
                {
                    return Results.BadRequest("Please replace string with a name and category");
                }
                if(product.price <= 0)
                {
                    return Results.BadRequest("Please write a price higher than 0");
                }
                return await next(invocationContext);
            });
            productGroup.MapGet("/", GetProducts);
            productGroup.MapGet("/{id}", GetAProduct);
            productGroup.MapPut("/{id}", UpdateProduct);
            productGroup.MapDelete("/{id}", DeleteProduct);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> AddProduct(IRepo<Product> repo, ProductPost product)
        {
            if (repo.GetAll().Any(x => x.name.Equals(product.name, StringComparison.OrdinalIgnoreCase)))
            {
                return Results.BadRequest("Product with provided name already exists");
            }

            
            var entity = new Product() { name =  product.name, category = product.category, price = product.price };
            repo.Add(entity);
            return TypedResults.Created($"/{entity.name}", entity);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetProducts(IRepo<Product> repo, string category)
        {
            if(!repo.GetAll().Any(x => x.category.Equals(category))) 
            {
                return TypedResults.NotFound("No products of the provided category were found");
            }
            var results = repo.GetAll().Where(x => x.category == category);
            return TypedResults.Ok(results);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetAProduct(IRepo<Product> repo, int id)
        {
            var productEntity = repo.GetAll().FirstOrDefault(x => x.id == id);
            if (productEntity != null)
            {
                return TypedResults.Ok(productEntity);
            }
            else
            {
                return TypedResults.NotFound("Not found");
            }
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> UpdateProduct(IRepo<Product> repo, int id, ProductPut updated)
        {
            
            if (repo.GetAll().Any(x => x.name == updated.name))
            {
                return TypedResults.BadRequest("Product with provided name already exists");
            }
            var prodEntity = repo.GetAll().FirstOrDefault(x => x.id == id);
            if (prodEntity == null) 
            {
                return TypedResults.NotFound("Not found");
            }
            
                prodEntity.name = updated.name;
                prodEntity.category = updated.category;
                prodEntity.price = updated.price;
            


            repo.Update(prodEntity);
            return TypedResults.Created($"/{prodEntity.name}", prodEntity);
            

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> DeleteProduct(IRepo<Product> repo, int id)
        {
            var prodEntity = repo.Delete(x => x.id == id);
            return prodEntity != null ? TypedResults.Ok(prodEntity) : TypedResults.NotFound("Not found");   
        }

    }
}
