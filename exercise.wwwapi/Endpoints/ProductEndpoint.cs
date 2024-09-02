using exercise.wwwapi.Controller;
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
            products.MapDelete("/DeleteProduct", DeleteProduct);


        }
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static IResult CreateProduct(IProduct<Product> view, Product product)
        {
            var entity = view.CreateProduct(product);
            if (entity == null)
            {
                return TypedResults.BadRequest("Price must be an integer, something else was provided. / Product with provided name already exists.");
            }

            return TypedResults.Ok(entity);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult GetAllProducts(IProduct<Product> view, string category)
        {
            var entity = view.GetAll(category);
            if (entity.Count == ProductController.products.Count)
            {
                return TypedResults.NotFound(entity);
            }
            return TypedResults.Ok(entity);
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult GetProduct(IProduct<Product> view, int id)
        {
            var entity = view.Get(id);

            if (entity == null)
            {
                return TypedResults.NotFound("Product not found");
            }
            return TypedResults.Ok(entity);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult UpdateProduct(IProduct<Product> view, Product newProduct, int id)
        {
           
            if(view.Get(id) == null)
            {
                return TypedResults.NotFound("Product not found");
            }
            if(newProduct.GetType() != typeof(int))
            {
                return TypedResults.BadRequest("Price must be an integer, something else was provided");
            }
            var entity = view.Update(newProduct, id);

            return TypedResults.Ok(entity);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult DeleteProduct(IProduct<Product> view, int id)
        {
            if(view.Get(id) == null)
            {
                return TypedResults.NotFound("Product not found");
            }
            var entity = view.Delete(id);

            return TypedResults.Ok(entity);
        }

    }
}
