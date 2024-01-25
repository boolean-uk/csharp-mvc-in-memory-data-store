using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoint
    {
        public static void ConfigureProductEndpoints(this WebApplication app)
        {
            var Products = app.MapGroup("Products");
            Products.MapGet("/{category?}", getAll);
            Products.MapGet("/singleProduct/", getAProduct);
            Products.MapPost("/", Add);
            Products.MapPut("/{Name}", updateProduct);
            Products.MapDelete("/{Name}", deleteProduct);
        }
        public static IResult getAll(IProductRepository Products, string? category) {
            try
            {
                if (category == null)
                {
                    return TypedResults.Ok(Products.getAll());    
                }
                var products = Products.getAll(category);
                if (products == null)
                {
                    return TypedResults.NotFound();
                }
                return TypedResults.Ok(products);
            }
            catch (Exception e){
                return TypedResults.BadRequest(e.Message); 
            }

        }
        public static IResult Add(IProductRepository Products, ProductPostPayload newProductData)
        {
            try
            {
                Product? item = Products.Add(newProductData.Product);
                if (item == null)
                {
                    return TypedResults.BadRequest("Product with Id {Id} not found.");
                }
                return TypedResults.Created($"/Products{item.Name}", item);
            }
            catch (Exception e)
            {
                return TypedResults.BadRequest(e.Message);
            }
        } 
        public static IResult getAProduct(IProductRepository Products, int Id)
        {
            Product? Product = Products.getAProduct(Id);
            if (Product == null)
            {
                return TypedResults.NotFound($"Product with Id {Id} not found.");
            }
            return TypedResults.Ok(Product);
        }
        public static IResult updateProduct(IProductRepository Products, int Id, ProductUpdatePayload newProductData)
        {
            string errorMsg;
            Product? item = Products.updateProduct(Id, newProductData.Product, out errorMsg);
            if (item == null && errorMsg == "type")
            {
                return TypedResults.BadRequest($"Price must be an integer, something else was provided");
            }
            if (item == null && errorMsg == "notfound")
            {
                return TypedResults.NotFound($"Product with Id {Id} not found.");
            }
            return TypedResults.Created($"/Products{item.Name}", item);
        }

        public static IResult deleteProduct(IProductRepository Products, int Id)
        {
            try
            {
                Product? item = Products.deleteAProduct(Id); 
                if (item == null)
                {
                    return TypedResults.NotFound($"Product with Id {Id} not found.");
                }
                return TypedResults.Ok(item);
            }
            catch (Exception e)
            {
                return TypedResults.BadRequest(e.Message); 
            }
        }
    }
}