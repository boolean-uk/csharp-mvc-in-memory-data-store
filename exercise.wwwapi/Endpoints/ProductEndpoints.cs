using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;
using exercise.wwwapi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoints
    {
        public static void configureProductEndpoints(this WebApplication app) 
        {
            var products = app.MapGroup("products");
            products.MapPost("/", Add);
            products.MapGet("/", getProducts);
            products.MapGet("/{id}", getProduct);
            products.MapPut("/{id}", Update);
            products.MapDelete("/{id}", Delete);

        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult getProducts(IRepo productrepo, string category)
        {
            var products = productrepo.GetProducts(category);
            if (products.Count == 0)
            {
                var error = new ErrorMessage("No products of the provided category were found.");
                return TypedResults.NotFound(error);
            }
            return TypedResults.Ok(products);
        }


        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static IResult Add(IRepo prodrepo, ProductsPostModel prod)
        {
            if (prod == null)
            {
                var error = new ErrorMessage("Price must be an integer, something else was provided");
                return TypedResults.BadRequest(error);
            }
            else if (prodrepo.GetAll().Any(x => x.Name == prod.Name))
            {
                var error = new ErrorMessage("Product with provided name already exists");
                return TypedResults.BadRequest(error);
            }
            prodrepo.CreateProduct(new Product() { Category = prod.Category, Name = prod.Name, Price = prod.Price });
            return TypedResults.Created();
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public static IResult getProduct(IRepo prodrepo, int id)
        {
            Product prod = prodrepo.GetProduct(id);
            if (prod == null)
            {
                var error = new ErrorMessage("Product not found.");
                return TypedResults.NotFound(error);
            }
            return TypedResults.Ok(prod);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static IResult Update(IRepo prodrepo, ProductsPostModel prodmodel, int id)
        {
            Product prod = new Product() { Category = prodmodel.Category, Name = prodmodel.Name, Price = prodmodel.Price};
            Product product = prodrepo.GetProduct(id);

            if (product == null)
            {
                var error = new ErrorMessage("Product not found.");
                return TypedResults.NotFound(error);
            }
            if (prod == null)
            {
                var error = new ErrorMessage("Price must be an integer, something else was provided.");
                return TypedResults.BadRequest(error);
            }
            else if (prodrepo.GetAll().Any(x => x.Name == prod.Name))
            {
                var error = new ErrorMessage("Product with provided name already exists.");
                return TypedResults.BadRequest(error);
            }

            prodrepo.UpdateProduct(prod, id);
            return TypedResults.Created();
        }



        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult Delete(IRepo prodrepo, int id)
        {
            Product product = prodrepo.GetProduct(id);
            if (product == null)
            {
                var error = new ErrorMessage("Product not found.");
                return TypedResults.NotFound(error);
            }

            prodrepo.DeleteProduct(id);

            return TypedResults.Ok();
        }
    }
}
