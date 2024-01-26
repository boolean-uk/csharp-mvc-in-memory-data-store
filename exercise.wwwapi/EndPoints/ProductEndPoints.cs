using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;

namespace exercise.wwwapi.EndPoints
{
    public static class ProductEndPoints
    {
        public static void ProductEndPointConfig(this WebApplication app)
        {
            var productGroup = app.MapGroup("products");

            productGroup.MapPost("/CreateNewProduct", CreateAProduct);
            productGroup.MapGet("/GetAllProducts", GetAllProducts);
            productGroup.MapGet("/GetAProduct{Id}", GetAProduct);
            productGroup.MapPut("/UpdateAProduct{Id}", UpdateAProduct);
            productGroup.MapDelete("/DeleteAProduct{Id}", DeleteAProduct);
        }

        public static IResult CreateAProduct(IProductRepository products, ProductPostPayload newProductData)
        {
            Product product = products.CreateAProduct(newProductData);
            return TypedResults.Ok(product);
        }

        public static IResult GetAllProducts(IProductRepository products)
        {
            return TypedResults.Ok(products.GetAllProducts());
        }

        public static IResult GetAProduct(IProductRepository products, int id)
        {
            return TypedResults.Ok(products.GetAProduct(id));
        }

        public static IResult UpdateAProduct(IProductRepository products, int id, ProductUpdateData updateData)
        {

            return TypedResults.Ok(products.UpdateAProduct(id, updateData));
        }
        public static IResult DeleteAProduct(IProductRepository products, int id)
        {
            return TypedResults.Ok(products.DeleteAProduct(id));
        }
    }

}
