using Microsoft.AspNetCore.Mvc;
using exercise.wwwapi.Repository;
using exercise.wwwapi.Models;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoint
    {
        public static void ConfigureProductEndpoint(this WebApplication app)
        {
            var productGroup = app.MapGroup("product");

            productGroup.MapPost("/", CreateProduct);
            productGroup.MapGet("/", GetProducts);
            productGroup.MapGet("/{id}", GetProductById);
            productGroup.MapPut("/{id}", UpdateProduct);
            productGroup.MapDelete("/{id}", DeleteProduct);
        }


        // get all products
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetProducts(IRepository repository)
        {
            var products = repository.GetProducts();
            if (products == null|| products.Count() == null)
            {
                return TypedResults.NotFound("No products of provided category found");

            }

            return TypedResults.Ok(products);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetProductById(IRepository repository, int id)
        {
            var found = repository.GetProduct(id);
            if (found == null)
            {
                return TypedResults.NotFound("Product not found");
            }

            return TypedResults.Ok(found);
        }

        // create a new product
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> CreateProduct(IRepository repository, ProductPost product)
        {
            if (!int.TryParse(product.Price, out var price))
            {
                return TypedResults.BadRequest(" Price most be an integer, something else was provided");
            }
  
            var newProduct = new Product() 
            { 
                Name = product.Name,
                Category = product.Category, 
                Price = price
            };

            if (newProduct == null) 
            {
                return TypedResults.BadRequest("Product with provided name allready exists");
            }

            repository.CreateProduct(newProduct);
            return TypedResults.Created($"/{newProduct.Name}", newProduct);
        }

        
        // update a product

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> UpdateProduct(IRepository repository, int id, ProductPut product)
        {
            if (!(product.Price is int))
            {
                return TypedResults.BadRequest("Price most be integer, something else was provided");
            }

            var newProduct = repository.UpdateProduct(id, product);


            if (newProduct == null)
            {
                return TypedResults.NotFound("Product not found");
            }

            return TypedResults.Created($"{product.Name}", newProduct);
        }



        // delete a product
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public static async Task<IResult> DeleteProduct(int id, IRepository repository)
        {
            var product = repository.DeleteProduct(id);
            if(product == null)
            {
                return TypedResults.NotFound(); 
            }

            return TypedResults.Ok(product);
    
        }







    }

}