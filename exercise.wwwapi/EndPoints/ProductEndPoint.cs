using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;

namespace exercise.wwwapi.EndPoints
{
    public static class ProductEndPoint
    {
        public static void ConfigureProductEndPoint(this WebApplication app)
        {
            var ProductGroup = app.MapGroup("/product");
            ProductGroup.MapGet("/", GetAllProduct);
            ProductGroup.MapPost("/", AddProduct);
            ProductGroup.MapGet("/{id}", GetProduct);
            ProductGroup.MapDelete("/{id}", DeleteProduct);
            ProductGroup.MapPut("/{id}", UpdateProduct);
        }

        public static IResult GetAllProduct(IProductRepository _productRepository)
        {
            var products = _productRepository.GetAllProducts();
            return Results.Ok(products);
        }

        public static IResult GetProduct(IProductRepository _productRepository, int id)
        {
            var product = _productRepository.GetProduct(id);
            if (product == null)
            {
                return Results.NotFound($"Product with id {id} was not found");
            }
            return Results.Ok(product);
        }

        public static IResult AddProduct(IProductRepository products, ProductPostPayload productPostPayload)
        {
            var product = products.AddProduct(productPostPayload.Name, productPostPayload.Category, productPostPayload.Price);
            return Results.Created($"/product/{product.Id}", product);
        }


        public static IResult UpdateProduct(IProductRepository products, int id, ProductUpdatePayload productUpdatePayload)
        {
            var product = products.UpdateProduct(id, productUpdatePayload);
            if (product == null)
            {
                return Results.NotFound($"Product with id {id} was not found");
            }
            return Results.Ok(product);
        }

        public static IResult DeleteProduct(IProductRepository products, int id)
        {
            var product = products.DeleteProduct(id);
            if (product == null)
            {
                return Results.NotFound($"Product with id {id} was not found");
            }
            return Results.Ok(product);
        }
    }
}