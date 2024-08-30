using exercise.wwwapi.Repositories;
using exercise.wwwapi.ViewModel;
using Microsoft.AspNetCore.Mvc;
using exercise.wwwapi.Model;
using Microsoft.AspNetCore.Http.HttpResults;

namespace exercise.wwwapi.EndPoints
{
    public static class ProductEndpoints
    {
        public static void ConfigureProductEndpoints(this WebApplication app)
        {
            var products = app.MapGroup("products");
            products.MapPost("", CreateProduct);
            products.MapGet("", GetAllProducts);
            products.MapGet("/{id}", GetAProduct);
            products.MapPut("/{id}", UpdateProduct);
            products.MapDelete("/{id}", DeleteProduct);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static IResult CreateProduct(IRepository repository, ProductModel product)
        {
            if (!int.TryParse(product.Price.ToString(), out int parsedPrice) || repository.GetAll().Any(p => p.Name == product.Name))  
            {
                var error = new ErrorMessage("Price must be an integer, something else was provided. / Product with provided name already exists.");
                return TypedResults.BadRequest(error);
            }

            repository.Add(new Product() { Name = product.Name, Category = product.Category, Price = parsedPrice });

            return TypedResults.Created();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult GetAllProducts(IRepository repository, string category)
        {
            if (repository.GetProducts(category).Count == 0)
            {
                var error = new ErrorMessage("No products of the provided category were found");
                return TypedResults.NotFound(error); 
            }

            return TypedResults.Ok(repository.GetProducts(category));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult GetAProduct(IRepository repository, int id)
        {
            Product product = repository.GetProduct(id);

            if (product == null)
            {
                var error = new ErrorMessage("Product not found.");
                return TypedResults.NotFound(error);
            }

            return TypedResults.Ok(product);
        }


        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult UpdateProduct(IRepository repository, int id, ProductModel newValues)
        {
            Product oldProduct = repository.GetProduct(id);

            if (oldProduct == null)
            {
                var error = new ErrorMessage("Product not Found.");
                return TypedResults.NotFound(error);
            }
            
            // Can't test if price is Null because it will fail before the test in the ProductModel when it does not have an integer value
            if (!int.TryParse(newValues.Price.ToString(), out int parsedPrice) || newValues.Name == oldProduct.Name)
            {
                var error = new ErrorMessage("Price must be an integer, something else was provided. / Product with provided name already exists.");
                return TypedResults.BadRequest(error);
            }

            Product newProductValues = new Product() { Name = newValues.Name, Category = newValues.Category, Price = parsedPrice };

            repository.Update(id, newProductValues);

            return TypedResults.Created();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult DeleteProduct(IRepository repository, int id)
        {
            Product product = repository.GetProduct(id);

            if (product == null)
            {
                var error = new ErrorMessage("Product not found.");
                return TypedResults.NotFound(error);
            }

            repository.Delete(id);
            return TypedResults.Ok();
        }

    }
}
