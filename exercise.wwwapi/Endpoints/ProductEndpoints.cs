using Microsoft.AspNetCore.Mvc;
using exercise.wwwapi.Model;
using exercise.wwwapi.Repository;



namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoints
    {

      

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult GetAllProducts(ProductRepository products, string category)
        {
            if (products.GetAllProducts(category) == null) 
            {
                return TypedResults.NotFound($"No product of the provided {category} were found.");
            }
            return TypedResults.Ok(products.GetAllProducts(category));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult GetProduct(ProductRepository products, int id)
        {
            Products? product = products.GetProduct(id);
            if (product == null)
            {
                return TypedResults.NotFound($"Product not found.");
            }
            return TypedResults.Ok(product);
        }



        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static IResult CreateProduct(ProductRepository products, string name, string category, string price)
        {
            if (!int.TryParse(price, out int parsedPrice))
            {
                return TypedResults.BadRequest("Price must be an integer, something else was provided.");
            }
            if (products.ProductExists(name))
            {
                return TypedResults.BadRequest("Product with provided name already exists.");
            }

            Products product1 = products.CreateProduct(name, category, parsedPrice);
            return TypedResults.Created($"/tasks/{product1.Id}", product1);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static IResult UpdateProduct(ProductRepository products, int id, string? newname, string? newcategory, int? newprice)
        {
            try
            {
                Products? product = products.UpdateProduct(id, newname, newcategory, newprice );
                if (product == null)
                {
                    return TypedResults.NotFound($"Product not found.");
                }
                return TypedResults.Ok(product);
            }
            catch (Exception e)
            {
                return TypedResults.BadRequest(e.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult DeleteProduct(ProductRepository products, int id)
        {
            if (!products.DeleteProduct(id))
            {
                return Results.NotFound("Product not found");
            }
            return Results.NoContent();
        }


    }
}

