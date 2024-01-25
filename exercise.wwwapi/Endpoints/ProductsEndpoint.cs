using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductsEndpoint
    {
        public static void ConfigureProductEndpoint(this WebApplication app)
        {
            var productGroup = app.MapGroup("products");
            productGroup.MapGet("/", GetProducts);
            productGroup.MapGet("/{id}", GetProduct);
            productGroup.MapPut("/{id}", UpdateProduct);
            productGroup.MapPost("/", AddProduct);
            productGroup.MapDelete("/{id}", DeleteProduct);

        }

        public static async Task<IResult> GetProducts(IRepository repository, string? category)
        {
            List<Product> products = repository.GetProducts(category);
            if (products.Count() > 0)
            {
                return TypedResults.Ok(repository.GetProducts(category));
            }
            return TypedResults.NotFound("Not found");
        }

        public static async Task<IResult> GetProduct(IRepository repository, int id)
        {
            if (!repository.IdExists(id))
            {
                return TypedResults.NotFound("Not found");
            }

            Product product = repository.GetProduct(id);
            return TypedResults.Ok(repository.GetProduct(id));
        }

        public static async Task<IResult> UpdateProduct(IRepository repository, int id, ProductBody product)
        {
            if (!repository.IdExists(id))
            {
                return TypedResults.NotFound("Not found");
            }
            Product change = repository.UpdateProduct(id, product);
            return TypedResults.Created("", change);
        }

        public static async Task<IResult> AddProduct(IRepository repository, ProductBody product)
        {
            return TypedResults.Created("/products", repository.AddProduct(product));
        }

        public static async Task<IResult> DeleteProduct(IRepository repository, int id)
        {
            if (!repository.IdExists(id))
            {
                return TypedResults.NotFound("Not found");
            }
            repository.DeleteProduct(id);
            return TypedResults.Ok();
        }
    }
}
