using exercise.wwwapi.Models.Discounts;
using exercise.wwwapi.Models.Products;
using exercise.wwwapi.Repositories.Discounts;
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

            // new endpoints
            productsGroup.MapPut("/{product_id}/attachDiscount/{discount_id}", AttatchDiscounToProduct);
            productsGroup.MapPut("/{product_id}/ditachDiscount", DetachDiscountFromProduct);
        }

        private static async Task<IResult> DetachDiscountFromProduct(int product_id, IProductRepository product)
        {
            bool isDetached = await product.RemoveDiscountFromProduct(product_id);
            return isDetached
                ? TypedResults.Ok(isDetached)
                : TypedResults.NotFound($"Product: {product_id} could not be found");
        }

        private static async Task<IResult> AttatchDiscounToProduct(int product_id, int discount_id, IProductRepository product)
        {
            bool isAttached = await product.AttachDiscountToProduct(product_id, discount_id);
            if (isAttached)
            {
                var tmpProduct = await product.getProductById(product_id);
                return TypedResults.Ok(tmpProduct);
            }
            
            return TypedResults.NotFound($"Product: {product_id} or Discount: {discount_id} could not be found");
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
