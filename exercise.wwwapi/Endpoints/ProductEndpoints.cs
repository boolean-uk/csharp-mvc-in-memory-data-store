using exercise.wwwapi.Model;
using exercise.wwwapi.Repository;
using System.Collections.Concurrent;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoints
    {
        public static void ConfigureProductEndpoints(this WebApplication app)
        {
            var productGroup = app.MapGroup("products");
            productGroup.MapGet("/", GetAllProducts);
            productGroup.MapGet("/{id}", GetProductByID);
            productGroup.MapPost("/", CreateProduct);
            productGroup.MapPut("/{id}", UpdateProduct);
            productGroup.MapDelete("/{id}", DeleteProduct);
        }

        public static IResult GetAllProducts(IProductRepository products)
        {
            return TypedResults.Ok(products.GetAll());
        }

        public static IResult GetProductByID(IProductRepository products, int id)
        {
            Product? result = products.GetProductByID(id);
            if (result is null) return TypedResults.NotFound();
            return TypedResults.Ok(result);
        }

        public static IResult CreateProduct(IProductRepository products, ProductPayload data)
        {
            return TypedResults.Created("", products.Create(data));
        }

        public static IResult UpdateProduct(IProductRepository products, int id, ProductPayload data)
        {
            Product? result = products.Update(id, data);
            if (result is null) return TypedResults.NotFound();
            return TypedResults.Created("", result);
        }

        public static IResult DeleteProduct(IProductRepository products, int id)
        {
            try
            {
                return TypedResults.Ok(products.Delete(id));
                
            }
            catch
            {
                return TypedResults.NotFound();
            }

        }
    }
}
