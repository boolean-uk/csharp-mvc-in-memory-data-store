using exercise.wwwapi.Model;
using exercise.wwwapi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoint
    {
        public static void ConfigureCarEndpoint(this WebApplication app)
        {
            var productGroup = app.MapGroup("products");

            //productGroup.MapGet("/", GetProducts);
            productGroup.MapGet("/", GetProduct);
            productGroup.MapPost("/", AddProduct);
            productGroup.MapGet("/{id}", GetProductByID);
            productGroup.MapPut("/{id}", UpdateProduct);
            productGroup.MapDelete("/{id}", DeleteProduct);

        }

        public static async Task<IResult> GetProducts(IRepository repository)
        {
            return TypedResults.Ok(repository.GetAllProducts());
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> DeleteProduct(IRepository repository, int id)
        {

            // return TypedResults.Ok(repository.DeleteProduct(id));
            var product = repository.DeleteProduct(id);

            if (product == null)
            {
                return TypedResults.NotFound("Product not found");
            }

            return TypedResults.Ok(product);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> UpdateProduct(IRepository repository, int id, ProductPut model)
        {
            //return TypedResults.Ok(repository.UpdateProduct(id, model));
            if (!int.TryParse(model.Price, out _))
            {
                return TypedResults.BadRequest("Price must be an integer, something else was provided");
            }

            var newProduct = repository.UpdateProduct(id, model);

            if (newProduct == null)
            {
                return TypedResults.NotFound("Product not found");
            }

            if (newProduct.Name == null)
            {
                return TypedResults.BadRequest("Product with provided name already exists");
            }

            return TypedResults.Created("/", newProduct);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetProductByID(IRepository repository, int id)
        {
            var product = repository.GetProductByID(id);
            if (product == null)
            {
                return TypedResults.NotFound("The product not found!");
            }

            return TypedResults.Ok(product);

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetProduct(IRepository repository, string category)
        {
            var products = repository.GetAllProducts(category.ToLower());

            if (products == null || products.Count() == 0)
            {
                return TypedResults.NotFound("No products found");
            }

            return TypedResults.Ok(products);


        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> AddProduct(IRepository repository, ProductPost model)
        {
            /**
            if (model == null)
            {

            }
            var newProduct = new Product() { Id = model.Id, Category = model.Category, Name = model.Name, Price = model.Price };
            repository.AddProduct(newProduct);
            return TypedResults.Created($"/{newProduct.Id}", newProduct);
            **/
            if (!int.TryParse(model.Price, out var price))
            {
                return TypedResults.BadRequest("Price must be an integer, something else was provided");
            }

            var newProduct = new Product() { Name = model.Name, Category = model.Category.ToLower(), Price = price };
            newProduct = repository.AddProduct(newProduct);

            if (newProduct == null)
            {
                return TypedResults.BadRequest("Product with provided name already exists");
            }

            return TypedResults.Created("/", newProduct);
        }
    }
}
