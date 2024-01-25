using exercise.wwwapi.Models;
using exercise.wwwapi.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoints
    {
        public static void ConfigureProductEndpoints(this WebApplication app)
        {
            var students = app.MapGroup("/products");

            students.MapPost("", AddProduct);
            //students.MapGet("", GetAllProducts);
            students.MapGet("", GetAllProducts);
            students.MapGet("/{id}", GetAProduct);
            students.MapPatch("/{id}", UpdateAProduct);
            students.MapDelete("/{id}", DeleteAProdcut);

        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static IResult AddProduct(IRepository products, ProductPayload payLoad)
        {
            //Check payload
            if (string.IsNullOrWhiteSpace(payLoad.Name)) { return TypedResults.BadRequest("Name can't be empty"); }
            if (payLoad.Price < 0) { return TypedResults.BadRequest("Price can't be negative"); }
            if (string.IsNullOrWhiteSpace(payLoad.Category)) { return TypedResults.BadRequest("Category can't be empty"); }

            //Check product
            var product = products.Add(payLoad);
            if (product == default) { return TypedResults.BadRequest("Product with provide name already exists"); }

            return TypedResults.Created($"/products/{product.Id}", product);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static IResult GetAllProducts(IRepository products, string category)
        {
            if (string.IsNullOrWhiteSpace(category))
            {
                if (products.GetAll() == null || products.GetAll().Count <= 0) { return TypedResults.NotFound("No products were found."); }
                return TypedResults.Ok(products.GetAll());
            }

            if (products.GetAll(category) == null || products.GetAll(category).Count <= 0) { return TypedResults.NotFound("No products of the provided category were found."); }
            return TypedResults.Ok(products.GetAll(category));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult GetAProduct(IRepository products, int id)
        {
            var product = products.Get(id);
            if (product == default) { return TypedResults.NotFound($"Product with id: {id} not found"); };
            return TypedResults.Ok(product);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult UpdateAProduct(IRepository products, int id, ProductPayload updatePayload)
        {
            if (new[] { updatePayload.Name, updatePayload.Price.ToString(), updatePayload.Category }
                .All(string.IsNullOrWhiteSpace)) { return TypedResults.BadRequest("Can't send empty payload"); }

            var product = products.Get(id);
            if (product == default) { return TypedResults.NotFound($"Product with id: {id} not found"); }

            product = products.Update(id, updatePayload);

            if (product == null) { return TypedResults.BadRequest("Product with name already exists."); }
            return TypedResults.Created(product.ToString());
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult DeleteAProdcut(IRepository products, int id)
        {
            var product = products.Delete(id);
            if (product == null) { return TypedResults.NotFound($"No product was found with id: {id}"); }
            return TypedResults.Ok(product);
        }
    }
}
