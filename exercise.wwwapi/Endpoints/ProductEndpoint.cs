using exercise.wwwapi.Controllers.Repository;
using exercise.wwwapi.Controllers;
using exercise.wwwapi.Controllers.Models;
using exercise.wwwapi.Controllers.Models.DTO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using static System.Reflection.Metadata.BlobBuilder;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoint
    {
        public static void ConfigureProductEndpoint(this WebApplication app)
        {
            var productgroup = app.MapGroup("products");
            productgroup.MapGet("/", getAll); //getall OR get all from a category
            productgroup.MapPost("/", Create); //add new product
            productgroup.MapGet("/{providedID}", GetbyID); //find product by ID
            productgroup.MapDelete("/{providedID}", Delete); //delete by ID
            productgroup.MapPut("/{providedID}", Update); //Update by ID
        }

        //create product
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> Create(IProductRepository<Product> productRepository, ProductPost newproduct)
        {
            var products = productRepository.getAll();

            if (products.Any(x => x.name.Equals(newproduct.name, StringComparison.OrdinalIgnoreCase)))
            {
                return Results.BadRequest("Product with provided name already exists");
            }
            else if (newproduct.price.GetType() != typeof(int)) 
            {
                return Results.BadRequest("Price must be an integer, something else was provided");
            }
            else
            {
                int id = products.Max(x => x.id) + 1;
                var entity = new Product(id, newproduct.name, newproduct.category, newproduct.price);
                products.Add(entity);
                return TypedResults.Created($"/{entity}");
            }
        }


        //get all or get all of a certain category 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> getAll(IProductRepository<Product> productRepository, string? providedCategory)
        {
            var results = productRepository.getAll();
            Payload<IEnumerable<Product>> payload = new Payload<IEnumerable<Product>>();
            //no category entered
            if (providedCategory == null) 
            {
                payload.data = results;
                return TypedResults.Ok(payload);
            }
            //category entered that exists
            else if (providedCategory != null && results.Any(p => p.category.ToLower() == providedCategory.ToLower()))
            {
                payload.data = productRepository.getAll(providedCategory);
                return TypedResults.Ok(payload);
            }
            else //category that doesnt exist
            {
                return TypedResults.NotFound("Not found.");
            }
        }


        //get a product by ID
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetbyID(IProductRepository<Product> repository, int providedID)
        {
            var products = repository.getAll();
            if (products.Exists(x => x.id == providedID))
            {
                var product = repository.find(providedID);
                return Results.Ok(product);
            }
            else
            {
                return Results.NotFound("Not found");
            }
        }


        //delete
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> Delete(IProductRepository<Product> repository, int providedID)
        {
            var products = repository.getAll();
            if (products.Exists(x => x.id == providedID))
            {
                var product = repository.find(providedID);
                repository.Delete(providedID);
                return Results.Ok(product);
            }
            return Results.BadRequest("Not found");
        }


        //update
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> Update(IProductRepository<Product> repository, int providedID, ProductPut prodUpdate)
        {
            var products = repository.getAll();

            if (products.Exists(x => x.id == providedID))
            {
                repository.Update(providedID, prodUpdate);
                var prod = products.Where(x => x.id == providedID).First();
                return TypedResults.Created($"/{prod.name}", prod);
            }
            return Results.BadRequest("Not found");
        }

    }
}
