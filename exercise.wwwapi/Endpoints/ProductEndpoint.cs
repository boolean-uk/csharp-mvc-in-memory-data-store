using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoint
    {
        public static void ConfigureProductEndpoints(this WebApplication app)
        {
            var productGroup = app.MapGroup("products");
            productGroup.MapGet("/GetAllProducts", GetAll);
            productGroup.MapGet("/GetProduct", GetProduct);
            productGroup.MapPost("/CreateProduct", CreateProduct);
            productGroup.MapPut("/UpdateProduct/{id}", UpdateProduct);
            productGroup.MapDelete("/DeleteProduct/{id}", DeleteProduct);
        }
        public static async Task<IResult> GetAll(IProductRepository products)
        {
            var product = await products.GetAll();
            return TypedResults.Ok(product);
        }
        public static IResult GetProduct(IProductRepository products, int id)
        {
            Product? product = products.GetProduct(id);
            if (product == null)
            {
                return TypedResults.NotFound($"No product with id {id} found.");
            }
            return TypedResults.Ok(product);
        }
        public static IResult CreateProduct(IProductRepository products, ProductPostPayload newProductData)
        {
            int discountAmount = 0;
            discountAmount = newProductData.discount;
            if (newProductData.name == null)
            {
                return TypedResults.BadRequest("name is required.");
            }
            if(newProductData.category == null)
            {
                return TypedResults.BadRequest("category is required.");
            }
            if(newProductData.price <= 0)
            {
                return TypedResults.BadRequest("price must be positive");
            }
            int newPrice = newProductData.price - discountAmount;
            Product product = products.AddProduct(newProductData.name, newProductData.category, newProductData.price);//Clip board: newPrice
            Discount discount = products.AddDiscount(discountAmount, product.Id);
            return TypedResults.Created($"/products{product.Id}", product);
        }
        public static IResult UpdateProduct(IProductRepository products, int id, ProductUpdatePayload updateData)
        {
            try
            {
                Product? product = products.UpdateProduct(id, updateData);
                if (product == null)
                {
                    return TypedResults.NotFound($"No product with {product.Id} found");
                }
                return TypedResults.Created($"/products{product.Name}", product);
            }
            catch (Exception e)
            {
                return TypedResults.BadRequest(e.Message);
            }
        }
        public static IResult DeleteProduct(IProductRepository products, int id)
        {
            return TypedResults.Ok(products.RemoveProduct(id));
        }
    }
}
