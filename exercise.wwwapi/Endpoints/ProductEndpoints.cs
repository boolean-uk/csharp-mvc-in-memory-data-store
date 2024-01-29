using exercise.wwwapi.Models.Products;
using exercise.wwwapi.Repositories.Producs;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoints
    {

        public static void ConfigureProductEndpoints(this WebApplication app)
        {
            var productsGroup = app.MapGroup("/products");
            productsGroup.MapGet("/", getAllProducts);
            productsGroup.MapGet("/{_id}", getProductById);
            productsGroup.MapPost("/", AddProduct);
            productsGroup.MapPut("/{_id}", UpdateProduct);
            productsGroup.MapDelete("({_id}", DeleteProduct);
        }

        private static async Task<IResult> DeleteProduct(int _id, IProductRepository product)
        {
            bool isDeleated = await product.DeleteProduct(_id);
            if (isDeleated)
            {
                return TypedResults.Ok();
            } else
            {
                return TypedResults.NotFound();
            }
        }

        private static async Task<IResult> UpdateProduct(int _id, IProductRepository product, ProductPutPayload payload)
        {
            Product updateProduct = await product.UpdateProduct(_id, payload);
            if (updateProduct == null)
            {
                return TypedResults.NotFound($"product {_id} could not be found");
            } else
            {
                return TypedResults.Created($"/products/{_id}", updateProduct);
            }
            
        }

        private static async Task<IResult> AddProduct(IProductRepository product, ProductPostPayload payload)
        {
            try
            {
                var result = await product.AddProduct(payload);
                if (result == null)
                {
                    return TypedResults.BadRequest("Invalid payload");
                } else
                {
                    return TypedResults.Created($"/products", result);
                }
                
            } catch (Exception ex)
            {
                return TypedResults.BadRequest(ex.Message);
            }
            
        }

        private static async Task<IResult> getProductById(int _id, IProductRepository product)
        {

            var foundProduct = await product.getProductById(_id);
            if (foundProduct == null)
            {
                return TypedResults.NotFound($"product: {_id} couls not be found");
            }else
            {
                return TypedResults.Ok(foundProduct);
            }

        }

        private static async Task<IResult> getAllProducts(IProductRepository product)
        {
            List<Product> tmp = await product.getAllProducts();
            if (tmp.Count == 0)
            {
                return TypedResults.NotFound("No Products could be found");
            } else
            {
                return TypedResults.Ok(product.getAllProducts());
            }
            
        }
    }
}
