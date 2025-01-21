
using Microsoft.AspNetCore.Mvc;
using exercise.wwwapi.Repository;
using exercise.wwwapi.Model;
using exercise.wwwapi.ViewModel;


namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoint
    {
        public static void ConfigureProductEndpoint(this WebApplication app)
        {
            var products = app.MapGroup("products");

            products.MapGet("products/{category}", Getproducts)
    .WithName("GetProductsByCategory");

            products.MapGet("products/{id:int}", Getproduct)
               .WithName("GetProductById");
            products.MapPost("/", AddProduct);
            products.MapDelete("/{id}", DeleteProducts);
            products.MapPut("/{id}", UpdateProduct);


        }
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> UpdateProduct(ProductRepository pr, int id, ProductPut model)
        {
            if (string.IsNullOrWhiteSpace(model.Name) || string.IsNullOrWhiteSpace(model.Category) || model.Price < 0)
            {
              
                return TypedResults.BadRequest("Invalid data: Name, category must not be empty, and price must be greater than 0.");
            }

            var product = await pr.UpdateProduct(id, model.Name, model.Category, model.Price);

            if (product == null)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(product);

            
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> DeleteProducts(ProductRepository pr, int id)
        {
            try
            {
                var product = await pr.GetProduct(id);
                if (product == null)
                {
                    return TypedResults.NotFound($"Product with ID {id} not found.");
                }

                await pr.DeleteProduct(id);
                return TypedResults.Ok($"Product with ID {id} has been deleted.");
            }
            catch (Exception ex)
            {
                return TypedResults.Problem($"An error occurred while deleting the product: {ex.Message}");
            }
        }
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        private static async Task<IResult> AddProduct(ProductRepository pr, ProductPost model)
        {
            try
            {
                Product product = new Product()
                {
                    Name = model.Name,
                    Category = model.Category,
                    Price = model.Price
                };
                await pr.AddProduct(product);
                return TypedResults.Created($"/products/{product.Id}", product);

            }
            catch (Exception ex)
            {
                return TypedResults.Problem(ex.Message);
            }
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> Getproduct(ProductRepository pr, int id)
        {
            var product=  await pr.GetProduct(id);
            if (product == null)
            {
                return TypedResults.NotFound(); 
            }


            return TypedResults.Ok(product);
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> Getproducts(ProductRepository pr, string category)
        {
           
            var products = (await pr.GetProducts(category)).ToList();

            if (products.Any())
            {
               
                return TypedResults.Ok(products);
            }

           
            var allProducts = (await pr.GetAll()).ToList();

            if (!allProducts.Any())
            {

                return TypedResults.NotFound("No products available.");
            }

           
            return TypedResults.Ok(allProducts);
        }
    }
}
