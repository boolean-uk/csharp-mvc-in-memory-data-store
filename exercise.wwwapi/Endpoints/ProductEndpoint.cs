using exercise.wwwapi.Model;
using exercise.wwwapi.View;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoint
    {

        public static void ConfigureProductEndpoint(this WebApplication app)
        {
            var products = app.MapGroup("products");
            products.MapPost("/CreateProduct", CreateProduct);
            products.MapGet("/GetAllProducts", GetAllProducts);
            products.MapGet("/GetProduct", GetProduct);
            products.MapPut("/UpdateProduct", UpdateProduct);


        }
        [ProducesResponseType(StatusCodes.Status201Created)]
        public static IResult CreateProduct(IProduct<Product> view, Product product)
        {

            return TypedResults.Ok(view.CreateProduct(product));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static IResult GetAllProducts(IProduct<Product> view)
        {
            return TypedResults.Ok(view.GetAll());
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult GetProduct(IProduct<Product> view, int id)
        {
            var entity = view.Get(id);

            if (entity == null)
            {
                return TypedResults.StatusCode(StatusCodes.Status404NotFound);
            }
            return TypedResults.Ok(entity);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        public static IResult UpdateProduct(IProduct<Product> view, Product newProduct, int id)
        {
            var entity = view.Update(newProduct, id);

            if(entity == null)
            {
                return TypedResults.StatusCode(StatusCodes.Status404NotFound);
            }
            return TypedResults.Ok(entity);
        }

    }
}
