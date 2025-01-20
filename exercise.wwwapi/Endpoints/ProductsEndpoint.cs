using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;
using exercise.wwwapi.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductsEndpoint
    {
        public static void ConfigureProductEndpoint(this WebApplication app)
        {
            var products = app.MapGroup("products");

            products.MapGet("/", GetProducts);
            products.MapGet("/{id}", GetProductById);
            products.MapPost("/{id}", AddProduct);
            products.MapDelete("/{id}", DeleteProduct);
            products.MapPut("/{id}", UpdateProduct);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetProducts(IRepository repository, string? category = null)
        {
            try
            {
                var products = await repository.GetAll(category);
                if (products == null || !products.Any()) return TypedResults.NotFound("No products of the provided category were found.");
                return TypedResults.Ok(products);
            }
            catch (Exception ex)
            {
                return TypedResults.Problem(ex.Message);
            } 
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetProductById(IRepository repository, int id)
        {
            try
            {
                var product = await repository.GetById(id);
                if (product == null) return TypedResults.NotFound("Product not found.");
                return TypedResults.Ok(product);
            }
            catch (Exception ex)
            {
                return TypedResults.Problem(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> AddProduct(IRepository repository, ProductPost model)
        {
            try
            {
                var target = await repository.GetByName(model.Name);
                if (target != null) return TypedResults.BadRequest("Product with provided name already exists.");
                Product product = new Product()
                {
                    Name = model.Name,
                    Category = model.Category,
                    Price = model.Price,
                };
                await repository.Add(product);
                return TypedResults.Created($"{product.Id}", product);
            }
            catch (Exception ex)
            {
                return TypedResults.Problem(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> DeleteProduct(IRepository repository, int id)
        {
            try
            {
                var model = await repository.GetById(id);
                if (repository.Delete(id) != null) return TypedResults.Ok(model);
                return TypedResults.NotFound("Product not found.");
            }
            catch (Exception ex)
            {
                return TypedResults.Problem(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> UpdateProduct(IRepository repository, int id, ProductPut model)
        {
            try
            {
                var target = await repository.GetById(id);
                if (target == null) return TypedResults.NotFound("Product not found.");

                if (!string.IsNullOrEmpty(model.Name))
                {
                    var existingProduct = await repository.GetByName(model.Name);
                    if (existingProduct != null && existingProduct.Id != id)
                    {
                        return TypedResults.BadRequest("A product with the provided name already exists.");
                    }
                    target.Name = model.Name;
                }
                if (!string.IsNullOrEmpty(model.Name)) target.Name = model.Name;
                if (!string.IsNullOrEmpty(model.Category)) target.Category = model.Category;               
                if (model.Price.HasValue) target.Price = model.Price.Value;
                await repository.Update(target);
                return TypedResults.Ok(target);
            }
            catch (Exception ex) 
            {
                return TypedResults.Problem(ex.Message);
            }
        }
    }
}
