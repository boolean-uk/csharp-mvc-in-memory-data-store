using exercise.wwwapi.Model;
using exercise.wwwapi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoint
    {
        //Extension method to configure the /languages endpoint
        public static void ConfigureProductEndpoint(this WebApplication app)
        {
            // Create a group for endpoints related to Products
            var productGroup = app.MapGroup("products");

            // Map different HTTP methods to their respective handlers
            productGroup.MapGet("/", GetProducts);
            productGroup.MapPost("/", AddProduct);
            productGroup.MapGet("/{id}", GetProduct);
            productGroup.MapPut("/{id}", UpdateProduct); //updating a product
            productGroup.MapDelete("/{id}", DeleteProduct); // deleting a product

        }
        // Handler for the GET request to retrieve all products
        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetProducts(IRepository<Product> repository, string? category)
        {
            if (category != null && !repository.Get().Any(x => x.Category == category)){
                return TypedResults.NotFound("No products of the provided category were found");
            }
            if (category == null)
            {
                return TypedResults.Ok(repository.Get());
            }
            var result = (repository.Get().Where(x => x.Category == category));

            return TypedResults.Ok(result);
        }



        // Handler for the POST request to create a new product
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> AddProduct(IRepository<Product> repository, ProductPost model)
        {
            // Check if a product with the provided name already exists
            if (repository.Get().Any(x => x.Name.Equals(model.Name, StringComparison.OrdinalIgnoreCase)))
            {
                return Results.BadRequest("Product with provided name already exists");
            }

            // Check if the price is an integer
            if (!int.TryParse(model.Price.ToString(), out var price))
            {
                return Results.BadRequest("Price must be an integer");
            }

            // Create a new product entity
            var entity = new Product { Name = model.Name, Category = model.Category, Price = model.Price };

            // Insert the new product into the repository
            repository.Insert(entity);

            // Return a 201 Created response with the added product's details
            return TypedResults.Created($"/{entity.Id}", entity);
        }

        // Handler for the GET request to retrieve a single product by ID
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetProduct(IRepository<Product> repository, int id)
        {
            // Retrieve the product with the provided ID from the repository
            var product = repository.GetById(id);

            // Check if the product was found
            if (product == null)
            {
                // If not found, return a 404 Not Found response with an error message
                return TypedResults.NotFound("Product not found.");
            }

            // If found, return a 200 OK response with the product details
            return TypedResults.Ok(product);
        }


        // Handler for the PUT request to update a product by ID
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> UpdateProduct(IRepository<Product> repository, int id, ProductPut model)
        {
            // Retrieve the product by ID
            var existingProduct = repository.GetById(id);

            // Check if the product was found
            if (existingProduct == null)
            {
                // If not found, return a 404 Not Found response with an error message
                return TypedResults.NotFound("Product not found.");
            }

            // Check if a product with the provided name already exists (excluding the current product)
            if (repository.Get().Any(x => x.Name.Equals(model.Name, StringComparison.OrdinalIgnoreCase) && x.Id != id))
            {
                return Results.BadRequest("Product with provided name already exists");
            }

            // Check if the price is an integer
            bool isInt = int.TryParse(model.Price.ToString(), out var price);
            if (isInt == false)
            {
                return Results.BadRequest("Price must be an integer");
            }

            // Update the product details
            existingProduct.Name = model.Name;
            existingProduct.Category = model.Category;
            existingProduct.Price = price;

            // Update the product in the repository
            repository.Update(existingProduct);

            // Return a 201 Created response with the updated product's details
            return TypedResults.Created($"/{existingProduct.Id}", existingProduct);
        }

        // Handler for the DELETE request to delete a product by ID
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> DeleteProduct(IRepository<Product> repository, int id)
        {
            // Retrieve the product by ID
            var existingProduct = repository.GetById(id);

            // Check if the product was found
            if (existingProduct == null)
            {
                // If not found, return a 404 Not Found response with an error message
                return TypedResults.NotFound("Product not found.");
            }

            // Delete the product from the repository
            repository.Delete(id);

            // Return a 200 OK response with the deleted product's details
            return TypedResults.Ok(existingProduct);
        }

    }
}
