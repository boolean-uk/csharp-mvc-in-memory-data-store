using exercise.wwwapi.Models;
using exercise.wwwapi.Data;
using exercise.wwwapi.Endpoints;
using exercise.wwwapi.Repository;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoint
    {
        public static void configureProductEndpoints(this WebApplication app)
        {
            var productGroup = app.MapGroup("products");

            productGroup.MapPost("/create", CreateNewProduct);

            productGroup.MapGet("/getAll", GetAllProducts);

            productGroup.MapGet("/getByID", GetProductByID);

            productGroup.MapPut("/update", UpdateProduct);

            productGroup.MapDelete("/delete", DeleteProduct);
        }

        public static IResult CreateNewProduct(IproductRepository products, string name, string category, int price) 
        {
            return TypedResults.Created("/created", products.CreateProduct(name, category, price));
        }

        public static IResult GetAllProducts(IproductRepository products)
        {
            return TypedResults.Ok(products.GetAllProducts());
        }

        public static IResult GetProductByID(IproductRepository products, int id)
        {
            return TypedResults.Ok(products.GetProductById(id));
        }

        public static IResult UpdateProduct(IproductRepository products, int id, string newName, string newCategory, int newPrice)
        {
            return TypedResults.Created("/updated", products.UpdateProduct(id, newName, newCategory, newPrice));
        }

        public static IResult DeleteProduct(IproductRepository products, int id)
        {
            return TypedResults.Ok(products.DeleteProduct(id));
        }
    }
}
