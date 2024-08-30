using exercise.wwwapi.Models;
using exercise.wwwapi.Repositories;
using exercise.wwwapi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Data;
using System.Runtime.InteropServices;

namespace exercise.wwwapi.EndPoints
{
    public static class ProductEndPoint
    {
        private static Message errorMSG = new Message("Not found.");
        public static void ConfigureProductEndPoints(this WebApplication app)
        {
            var products = app.MapGroup("products");
            products.MapGet("/", GetAllProducts);
            products.MapGet("/{id:int}", GetSingleProduct);
            products.MapPost("/", AddProduct);
            products.MapPut("/{id:int}", UpdateProduct);
            products.MapDelete("/{id:int}", DeleteProduct);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult GetAllProducts(IRepository repo, [Optional] string category)
        {
            List<Product> productList = repo.GetProducts(category);
            if(productList.Count == 0)
            {
                return TypedResults.NotFound(errorMSG);
            }
            return TypedResults.Ok(productList);
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult GetSingleProduct(IRepository repo, int id)
        {
            Product product = repo.GetSingleProduct(id);
            if(product == null)
            {
                return TypedResults.NotFound(errorMSG);
            }
            return TypedResults.Ok(product);
        }


        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static IResult AddProduct(IRepository repo, ProductPostModel model)
        {
            bool productAlreadyExists = repo.ProductExists(model.Name);
            if (productAlreadyExists)
            {
                return TypedResults.BadRequest(errorMSG);
            }
            Product newProduct = repo.AddProduct(new Product() { Name = model.Name, Category = model.Category, Price = model.Price });
            return TypedResults.Created("", newProduct);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult UpdateProduct(IRepository repo, int id, ProductPostModel model)
        {

            Product updatedProduct = new Product() { Name = model.Name, Category = model.Category, Price = model.Price };
           
            updatedProduct = repo.UpdateProduct(id, updatedProduct);
            if(updatedProduct == null)
            {
                return TypedResults.NotFound(errorMSG);
            }

            return TypedResults.Created("", updatedProduct);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult DeleteProduct(IRepository repo, int id)
        {
            Product productToBeDeleted = repo.GetSingleProduct(id);
            if(productToBeDeleted == null)
            {
                return TypedResults.NotFound(errorMSG);
            }
            repo.RemoveProduct(productToBeDeleted);
            return TypedResults.Ok(productToBeDeleted);
        }

    }
}
