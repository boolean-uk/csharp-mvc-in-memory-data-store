using exercise.wwwapi.Data;
using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;

namespace exercise.wwwapi.Endpoints
{
    public class ProductEndpoints
    {
        public static IResult GetAllProducts (ProductRepository ProductRepo)
        {
            var Products = ProductRepo.GetAllProducts();
            return TypedResults.Ok(Products);
        }
        public static IResult AddProduct(ProductRepository ProductRepo, string Name, string Category, string Price, ErrorCollection errors)
        {
            Product? newProduct = ProductRepo.AddProduct(Name, Category, Price);

            if(newProduct == null) 
            {
                return TypedResults.BadRequest(errors.GetError(400));
            }

            return TypedResults.Created($"/Products{newProduct.id}", newProduct);
        }

        public static IResult UpdateProduct(ProductRepository ProductRepo, int id, ProductUpdatePayload updateData)
        {
            try
            {
                Product? Product = ProductRepo.UpdateProduct(id, updateData);
                if(Product == null)
                {
                    return Results.NotFound($"Product with id {id} not found");
                }
                return Results.Created($"Product with id {id} updated", Product);
            }
            catch(Exception e)
            {
                return Results.BadRequest(e.Message);
            }


        }

        public static IResult GetProduct(ProductRepository ProductRepo, int id)
        {
            Product? Product = ProductRepo.GetProduct(id);

            if(Product == null)
            {
                return Results.NotFound($"Product with id {id} not found");
            }

            return TypedResults.Ok(Product);
        }

        public static IResult DeleteProduct(ProductRepository Products, int id)
        {
            Product? Product = Products.GetProduct(id);

            if(Product == null)
            {
                return Results.NotFound($"Product with id {id} not found");
            }

            return TypedResults.Ok(Products.RemoveProduct(id));
        }

    }
}
