using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoint
    {
        public static void ProductLogics(this WebApplication app)
        {
            var productGroup = app.MapGroup("products");
            productGroup.MapGet("/", GetAllProducts);
            productGroup.MapGet("/{id}", GetProduct);
            productGroup.MapPost("/", CreateProduct);
            productGroup.MapPut("/{id}", UpdateProduct);
            productGroup.MapDelete("/{id}", DeleteProduct);
        }

        public async static Task<IResult> GetAllProducts(IProductRepository products)
        {
            List<Product> productsList = await products.GetAllProducts();
            if (productsList.Count() == 0)
                return TypedResults.NotFound();

            return TypedResults.Ok(productsList);
        }

        public async static Task<IResult> GetProduct(IProductRepository products, int id)
        {
            Product? product = await products.GetProduct(id);
            if (product == null)
                return Results.NotFound("ID out of scope");

            return TypedResults.Ok(products.GetProduct(id));
        }

        public async static Task<IResult> CreateProduct(IProductRepository products, ProductPostPayload createdProduct)
        {
            Product? product = await products.AddProduct(createdProduct.name, createdProduct.catagory, createdProduct.price);
            if (product == null)
                return Results.BadRequest("Name already found");

            return TypedResults.Created($"/products{product.Name} {product.Catagory} {product.Price}", product);
        }

        public async static Task<IResult> UpdateProduct(IProductRepository products, ProductUpdatePayload posted, int id)
        {
            Product? product = await products.GetProduct(id);
            if (product == null)
                return Results.NotFound("ID out of scope");

            product = await products.UpdateProduct(product, posted);
            if (product == null)
                return Results.BadRequest("Name already found");

            return TypedResults.Created($"/products{product.Name} {product.Catagory} {product.Price}", product);
        }

        public async static Task<IResult> DeleteProduct(IProductRepository products, int id)
        {
            Product? product = await products.GetProduct(id);
            if (product == null)
                return Results.NotFound("ID out of scope");

            return TypedResults.Ok(products.DeleteProduct(id));
        }
    }
}
