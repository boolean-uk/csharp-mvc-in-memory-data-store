using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoints
    {
        public static void ConfigureProductEndpoint(this WebApplication app)
        {
            // THIS IS JUST A MAP GET
            var productGroup = app.MapGroup("products");
            productGroup.MapGet("/", GetProducts);
            productGroup.MapPost("/", AddProduct);
            productGroup.MapPut("/{id}", UpdateProduct);
            productGroup.MapGet("/{id}", GetAProduct);
            productGroup.MapDelete("/{id}", DeleteProduct);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> AddProduct(IRepository<Products> repository, [FromBody] ProductPost inputProduct)
        {
            if (inputProduct == null)
            {
                return TypedResults.BadRequest(new { message = "Price must be an integer, something else was provided. / Product with provided name already exists." });
            }

            Products model = new Products() { name = inputProduct.name, category = inputProduct.category, price = inputProduct.price };
            repository.Add(model);
            return TypedResults.Created($"/{model.name}", model);
            
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> GetProducts(IRepository<Products> repository,  [FromQuery] string? inputCategory)     //? 
        {
            if (inputCategory == null)
            {
                return TypedResults.Ok(repository.GetAll());
            }
            else { 
                var _products = repository.Get(inputCategory);

                if (_products == null)
                {
                    return Results.NotFound(new { message = "No products of the provided category were found" });
                }
                else return TypedResults.Ok(_products);   
            }
           
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetAProduct(IRepository<Products> repository, int id)   
        {
            if (id == null)
            {
                return TypedResults.BadRequest();
            }
            else {
                var _product = repository.GetById(id);
                if (_product == null)
                {
                    return Results.NotFound(new { message = "Product not found." });
                }
                else {
                    return TypedResults.Ok(_product);
                }
            }
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> UpdateProduct(IRepository<Products> repository, [FromQuery] int? id, [FromBody] ProductPost updatedProduct)   //Not using ProductPost for now...
        {
            if (id == null)
            {
                return TypedResults.BadRequest();
            }
            else
            {
                var _product = repository.Update(updatedProduct, id.Value);
                if (_product == null)
                {
                    return Results.NotFound(new { message = "Product not found." });
                }
                else
                {
                    return TypedResults.Created($"/{1}", _product);
                }
            }

        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> DeleteProduct(IRepository<Products> repository, [FromQuery] int? id )
        {
            if (id == null)
            {
                return TypedResults.BadRequest();
            }
            else
            {
                var _existing = repository.GetById(id.Value);
                if (_existing == null)
                {
                    return Results.NotFound(new { message = "Product not found." });
                }
                else
                {
                    var deletedProduct = repository.Delete(id.Value);
                    return TypedResults.Ok(deletedProduct);
                }
            }

        }
    }

}

