using System.Diagnostics;
using System.Text.Json;
using exercise.wwwapi.ViewModels;
using genericapi.api.Models;
using genericapi.api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Endpoints
{
    public static class ConfigureProductEndpoints
    {
        public static string Path { get; } = "products";
        public static void ConfigureProductEndpoint(this WebApplication app)
        {
            var products = app.MapGroup(Path);

            products.MapGet("/", GetProducts);
            products.MapGet("/{id}", GetProduct);
            products.MapPost("/", PostProduct);
            products.MapPut("/{id}", PutProduct);
            products.MapDelete("/{id}", DeleteProduct);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetProducts(IRepository<Product, Guid> repository, string? category)
        {
            try
            {
                var result = await repository.GetAll();
                if (string.IsNullOrEmpty(category))
                    return TypedResults.Ok(result);
                result = result.Where(x => x.Category.Contains(category, StringComparison.OrdinalIgnoreCase));
                if (!result.Any()) return TypedResults.NotFound(new {Message = "No products of the provided category was found"});
                return TypedResults.Ok(result);

            }
            catch (Exception ex)
            {
                return TypedResults.Problem(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetProduct(IRepository<Product, Guid> repository, Guid id)
        {
            try
            {
                return TypedResults.Ok(await repository.Get(id));
            }
            catch (ArgumentException ex)
            {
                return TypedResults.NotFound(new { ex.Message });
            }
            catch (Exception ex)
            {
                return TypedResults.Problem(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> PostProduct(IRepository<Product, Guid> repository, ProductPost entity)
        {
            try
            {
                var products = await repository.GetAll();
                if (products.Any(p => p.Name.Equals(entity.Name, StringComparison.OrdinalIgnoreCase)))
                    return TypedResults.BadRequest(new { Message =  $"Product {entity.Name} already exists!" });

                Product product = await repository.Add(new Product { Name = entity.Name, Category = entity.Category, Price = entity.Price });
                return TypedResults.Created($"/{Path}/{product.Id}", product);
            }
            catch (Exception ex)
            {
                return TypedResults.Problem(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> PutProduct(IRepository<Product, Guid> repository, Guid id, ProductPut entity) 
        {
            try
            {
                var products = await repository.GetAll();
                if (entity.Name != null && products.Any(p => p.Name.Equals(entity.Name, StringComparison.OrdinalIgnoreCase)))
                    return TypedResults.BadRequest(new { Message = $"Product {entity.Name} already exists!" });
                Product product = await repository.Get(id);
                if (entity.Name != null) product.Name = entity.Name;
                if (entity.Category != null) product.Category = entity.Category;
                if (entity.Price != null) product.Price = entity.Price.Value;

                product = await repository.Update(product);
                return TypedResults.Created($"/{Path}/{product.Id}", product);
            } catch (ArgumentException ex)
            {
                return TypedResults.NotFound(new { ex.Message });
            }
            catch (Exception ex)
            {
                return TypedResults.Problem(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> DeleteProduct(IRepository<Product, Guid> repository, Guid id)
        {
            try
            {
                Product product = await repository.Get(id);
                return TypedResults.Ok(new { Deleted = await repository.Delete(id), Name = $"{product.Name} - {product.Category}" });
            }
            catch (ArgumentException ex)
            {
                return TypedResults.NotFound(new { ex.Message });
            }
            catch (Exception ex)
            {
                return TypedResults.Problem(ex.Message);
            }
        }
    }
}
