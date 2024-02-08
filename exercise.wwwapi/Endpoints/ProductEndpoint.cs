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

            productGroup.MapGet("/getByCategory", GetProductsByCategory);

            productGroup.MapPut("/update", UpdateProduct);

            productGroup.MapDelete("/delete", DeleteProduct);
        }

        //create new product
        public static async Task<IResult> CreateNewProduct(IproductRepository products, ProductPayload updateData) 
        {
            var product = await products.CreateProduct(updateData);
            if (product == null)
            {
                return TypedResults.BadRequest( "Product with the same name already exists");
            }
            return TypedResults.Created("/created", product);
        }

        //get all products
        public static async Task<IResult> GetAllProducts(IproductRepository products)
        {
            return TypedResults.Ok(await products.GetAllProducts());
        }

        //get products by category
        public static async Task<IResult> GetProductsByCategory(IproductRepository products, string category)
        {
            var results = await products.GetProductsByCategory(category);
            if (results == null)
            {
                return TypedResults.NotFound("no products with this category exists");
            }
            return TypedResults.Ok(results);
        }

        //get product by ID
        public static async Task<IResult> GetProductByID(IproductRepository products, int id)
        {
            var result = await products.GetProductById(id);
            if (result == null)
            {
                return TypedResults.NotFound("no products with this ID");
            }
            return TypedResults.Ok(await products.GetProductById(id));
        }

        //update product
        public static async Task<IResult> UpdateProduct(IproductRepository products, int id, ProductPayload updateData)
        {
            var results = await products.UpdateProduct(id, updateData);
            if (results == null)
            {
                return TypedResults.BadRequest("product with provided name already exists," +
                    "or no product with provided ID");
            }
            return TypedResults.Created("/updated", results);
        }

        //delete product
        public static async Task<IResult> DeleteProduct(IproductRepository products, int id)
        {
            var results = await products.DeleteProduct(id);
            if (results == null)
            {
                return TypedResults.NotFound(TypedResults.NotFound());
            }
            return TypedResults.Ok(results);
        }
    }
}
