using exercise.wwwapi.Objects;
using exercise.wwwapi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Endpoint
{
    public static class ProductEndpoints
    {
        public static void ConfigureProductEndpoint(this WebApplication app)
        {
            var carGroup = app.MapGroup("products");

            carGroup.MapGet("/", GetProduct);
            carGroup.MapPost("/", CreateProduct);
            carGroup.MapPut("/{id}", UpdateProduct);
            carGroup.MapGet("/{id}", GetAProduct);
            carGroup.MapDelete("/{id}", DeleteProduct);
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        public static IResult GetProduct(IRepository Repository, string? category)
        {
            return TypedResults.Ok(Repository.GetProducts(category));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult GetAProduct(IRepository repository, int id)
        {
            Product? item = repository.GetProductById(id);
            if (item == null)
            {
                return TypedResults.NotFound($"Product with id {id} not found.");
            }
            return TypedResults.Ok(item);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static IResult CreateProduct(IRepository repository, InputProduct newProduct)
        {
            if (newProduct == null) return TypedResults.BadRequest("Product is required.");

            //Error handling for when user input's values are not the same type as InputProduct
            if (!int.TryParse(newProduct.Price,out int value)) return TypedResults.BadRequest("Price is a string instead of an int");

            Product item = repository.CreateProduct(newProduct);
            if (item == null) return TypedResults.BadRequest("Product with same name exists");
            return TypedResults.Created($"/tasks{item.Id}", item);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static IResult UpdateProduct(IRepository repository, int id, InputProduct updateProduct)
        {
            try
            {
                //Error handling for when user input's values are not the same type as InputProduct
                if (!int.TryParse(updateProduct.Price, out int value)) return TypedResults.BadRequest("Price is a string instead of an int");

                Product ExistingProduct = repository.GetProductById(id);

                if (repository.GetProducts("").Any(t => t.Name == updateProduct.Name && t.Id != id)) { return TypedResults.BadRequest("Name already exists"); }

                Product? item = repository.UpdateProduct(updateProduct,id);
                if (item == null)
                {
                    return TypedResults.NotFound($"Product with id {id} not found.");
                }
                return TypedResults.Ok(item);
            }
            catch (Exception e)
            {
                return TypedResults.BadRequest(e.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult DeleteProduct(IRepository repository, int id)
        {
            Product? item = repository.DeleteProduct(id);
            if (item == null)
            {
                return TypedResults.NotFound($"Product with id {id} not found.");
            }
            return TypedResults.Ok(item);
        }
    }
}
