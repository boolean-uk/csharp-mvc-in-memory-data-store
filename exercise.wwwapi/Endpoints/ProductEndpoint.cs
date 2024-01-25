using exercise.wwwapi.DAL;
using exercise.wwwapi.Model;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoint
    {
        public static void ConfigureProductEndpoint(this WebApplication app)
        {
            var languageGroup = app.MapGroup("products");
            languageGroup.MapPost("/", Create);    
            languageGroup.MapGet("/", Get);
            languageGroup.MapGet("/{id}", GetProduct);
            languageGroup.MapPut("/{id}", UpdateProduct);
            languageGroup.MapDelete("/{id}", DeleteProduct);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        public static async Task<IResult> Create(IRepository repository, Product product)
        {
            try 
            {
                Product item = repository.Add(product);
                return TypedResults.Created("products/", item);

            }
            catch (Exception ex) 
            {
                Debug.WriteLine(ex);
                return TypedResults.BadRequest("price must be an integer, something else was provided. / product " +
                    "with provided name already exists.");
            }            
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> Get(IRepository repository, string? category)
        {
            try 
            {
                List<Product> products = repository.GetProducts(category);
                return TypedResults.Ok(products);
            }
            catch (Exception e) 
            {
                Debug.WriteLine($"Error: {e}");
                return TypedResults.NotFound();
            }           
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetProduct(IRepository repository, int id)
        {
            try
            {
                Product product = repository.Get(id);                
                return TypedResults.Ok(product);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error: {e}");
                return TypedResults.NotFound();
            }
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        public static async Task<IResult> UpdateProduct(IRepository repository, int id, Product product)
        {
            try
            {
                int parsed;
                bool parsedOK = int.TryParse(id.ToString(), out parsed);
                if (!parsedOK) { return TypedResults.BadRequest(); }

                Product item = repository.Update(product, id);
                return TypedResults.Ok(item);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error: {e}");
                return TypedResults.NotFound();
            }            
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> DeleteProduct(IRepository repository, int id)
        {
            try
            {
                Product product = await repository.Delete(id);
                return TypedResults.Ok(product);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error: {e}");
                return TypedResults.NotFound();
            }
        }
    }
}
