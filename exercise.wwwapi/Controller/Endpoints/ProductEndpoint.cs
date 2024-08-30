using exercise.wwwapi.Controller.Repository;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Controller.Endpoints
{
    public static class ProductEndpoint
    {
        public static void ConfigureProductEndpoint(this WebApplication application)
        {
            var productGroup = application.MapGroup("Products");
            productGroup.MapGet("/", GetAllProducts);
            productGroup.MapGet("/{id}", GetAProduct);
            productGroup.MapPost("/", AddProduct);
            productGroup.MapPut("/{id}", UppdateProduct);
            productGroup.MapDelete("/{id}", DeleteProduct);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static IResult GetAllProducts(IProductRepository repository)
        {
            return TypedResults.Ok(repository.GetAllProducts());
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static IResult DeleteProduct(IProductRepository repository, int id)
        {
            return TypedResults.Ok(repository.DeleteProduct(id));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static IResult GetAProduct(IProductRepository repository, int id)
        {
            return TypedResults.Ok(repository.GetAProduct(id));
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        public static IResult


    }
}
