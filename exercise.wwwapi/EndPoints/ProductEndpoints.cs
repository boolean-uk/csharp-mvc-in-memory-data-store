using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;
using exercise.wwwapi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace exercise.wwwapi.EndPoints
{
    public static class ProductEndpoints
    {

        public static void ConfigureProductEndpoint(this WebApplication app)
        {
            var product = app.MapGroup("product");
            product.MapPost("/", AddProduct);
            product.MapGet("/", GetProducts);
            product.MapGet("/{id}", GetaAProduct);
            product.MapPut("/{id}", UpdateProduct);
            product.MapDelete("/{id}", DeleteProduct);
        }

       

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static IResult AddProduct(IRepository repository, ProductPostModel model)
        {

            Product product = repository.AddProduct(new Product() { Name = model.Name, Category = model.Category, Price = model.Price });
            if (product.Price.GetType() == typeof(int))
            {
                return TypedResults.Ok(product);
            }
            else
            {
                return TypedResults.BadRequest("Price needs to be a integer!!!");
            }
            
            
        }
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult GetProducts(IRepository repository, string category)
        {
            return TypedResults.Ok(repository.GetAllProducts(category));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static IResult GetaAProduct(IRepository repository, int Id)
        {
            return TypedResults.Ok(repository.GetAProduct(Id));
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static IResult UpdateProduct(IRepository repository, int Id, ProductPostModel model)
        {
            Product product = repository.UpdateProduct(new Product() { Name = model.Name, Category = model.Category, Price = model.Price }, Id);
            return TypedResults.Ok(product);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static IResult DeleteProduct(IRepository repository, int Id)
        {
            return TypedResults.Ok(repository.DeleteProduct(Id));
        }
    }
}
