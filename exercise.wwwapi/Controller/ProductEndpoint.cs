using exercise.wwwapi.Model;
using exercise.wwwapi.Repositories;
using exercise.wwwapi.ViewModel;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace exercise.wwwapi.Controller
{
    public static class ProductEndpoint
    {

        public static void ConfigureProductEndpoints(this WebApplication app)
        {
            var products = app.MapGroup("products");
            products.MapGet("/get", GetProducts);
            products.MapGet("/get/{id}", GetAProduct);
            products.MapPost("/create", CreateAProduct);
            products.MapPut("/edit", EditProduct);
            products.MapDelete("/delete/{id}", DeleteProduct);
        }


        public static IResult DeleteProduct(int id, IRepository repository)
        {
            var deletedElement = repository.delete(id);
            if (deletedElement == null)
            {
                repository.message = "Not Found";
                return Results.Json(repository.message,statusCode : 404);
            }

            return Results.Ok(deletedElement);
            
        }

        public static IResult EditProduct(int id, IRepository repository, ProductViewModel product)
        {
           var updateProduct =  repository.update(id,product);
            if (id < 0 ) 
            {
                repository.message = "Id not valid";
                return Results.Json(repository.message, statusCode: 400);
            }
            
            else if (!repository.checkIfExists(updateProduct.name))
            {
                repository.message = "Name Already exists";
                return Results.Json(repository.message, statusCode: 400);   
            }
            else if (updateProduct == null)
            {
                repository.message = "Not Found";
                return Results.Json(repository.message, statusCode: 400);
            }

            return Results.Ok(updateProduct);

        }


        public static IResult CreateAProduct ([FromBody] ProductViewModel product,IRepository repository)
        {
            var createdProduct = repository.create(new Product() { name = product.name, price = product.price });

            if(product.price <0)
            {
                repository.message = "Price invalid";
                return Results.Json(repository.message, statusCode: 404);
            }
            else if (createdProduct == null)
            {
                repository.message = "Not Found";
                return Results.Json(repository.message, statusCode: 404);
            }
            else if (!repository.checkIfExists(product.name))
            {
                repository.message = "Product already exists";
                return Results.Json(repository.message, statusCode: 404);
            }
            return TypedResults.Ok(createdProduct);
        }



        public static IResult GetAProduct(int id, IRepository repository)
        {
            var product = repository.get(id);
           
            if (product == null)
            {
                repository.message = "Not Found";
                return Results.Json(repository.message, statusCode: 404);
            }
            else
            {
                return TypedResults.Ok(product);
            }

        }
        public static IResult GetProducts(IRepository repository)
        {
            var productList = repository.getAll();
            
            if (productList == null || productList.Count==0)
            {
                repository.message = "Not Found";
                return Results.Json(repository.message, statusCode: 404);
            }
            else
            {
                return TypedResults.Ok(productList);
            }
        }
    }
}
