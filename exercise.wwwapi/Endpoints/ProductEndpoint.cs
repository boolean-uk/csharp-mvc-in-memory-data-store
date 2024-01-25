using Microsoft.AspNetCore.Mvc;
using exercise.wwwapi.Repository;
using exercise.wwwapi.Products;
namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoint
    {
        public static void ConfigureProductEndpoint(this WebApplication app)
        {
            var productGroup = app.MapGroup("products");

            productGroup.MapPost("/create a product", AddProduct);
            productGroup.MapGet("/get all products", GetProducts);
            productGroup.MapGet("/{id}", AddProduct);
            productGroup.MapPut("/update a product {name}", UpdateProduct);
            productGroup.MapDelete("/delete product{id}", DeleteProduct);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetProducts([FromServices] IRepository repository)
        {
            return TypedResults.Ok(repository.GetProducts());
        }


        [ProducesResponseType(StatusCodes.Status201Created)]
        public static async Task<IResult> AddProduct([FromServices] IRepository repository, [FromBody] ProductPost product)
        {
            //validate
            if (product == null)
            {

            }
            var newProduct = new Product() { Category = product.Category, Price = product.Price };
            repository.Add(newProduct);
            return TypedResults.Created($"/{newProduct.Name}", newProduct);
        }

          
        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> UpdateProduct([FromServices] IRepository repository, int id, [FromBody] ProductPut name)
        {
            return TypedResults.Ok(repository.Update(id, name));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> DeleteProduct(int id, IRepository repository)
        {
            var studentDeleted = repository.Delete(id);
            return TypedResults.Ok(studentDeleted);
    
        }







    }

}