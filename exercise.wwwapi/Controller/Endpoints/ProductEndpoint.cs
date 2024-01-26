using exercise.wwwapi.Model.Models;
using exercise.wwwapi.Model.Repository;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Controller.Endpoints
{
    public static class ProductEndpoint
    {
        public static void ConfigureProductEndpoint(this WebApplication app)
        {
            var productGroup = app.MapGroup("products");
            productGroup.MapGet("/", GetProducts);
            productGroup.MapGet("/{id}", GetProduct);
            productGroup.MapPost("/", AddProduct);
            productGroup.MapPut("/{id}", UpdateProduct);
            productGroup.MapDelete("/{id}", DeleteProduct);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetProducts(IRepository<Product> repository)
        {
            return TypedResults.Ok(repository.GetAll());
        }

        [ProducesResponseType(StatusCodes.Status200OK)]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetProduct(IRepository<Product> repository, int id)
        {
            if (!IsAnExistingId(repository, id)) return Results.NotFound("No products with the the provided ID could be found.");
            return TypedResults.Ok(repository.Get(id));
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> AddProduct(IRepository<Product> repository, ProductCreate product)
        {
            if (IsAnExistingName(repository, product.Name))
            {
                return Results.BadRequest("There is already a product with the provided name.");
            }
            var newProduct = new Product(product.Name, product.Category, product.Price);
            repository.Insert(newProduct);
            return TypedResults.Created($"{newProduct.Id}", newProduct);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> UpdateProduct(IRepository<Product> repository, int id, ProductCreate product)
        {
            if (!IsAnExistingId(repository, id)) return Results.NotFound("No products with the the provided ID could be found.");
            if (IsAnExistingName(repository, product.Name) && !(repository.GetAll().FirstOrDefault(x => x.Name == product.Name).Id == id))
            {
                return Results.BadRequest("There is already a product with the provided name.");
            }
            var productToUpdate = repository.Get(id);
            productToUpdate.Name = product.Name != null ? product.Name : productToUpdate.Name;
            productToUpdate.Category = product.Category != null ? product.Category : productToUpdate.Category;
            productToUpdate.Price = product.Price != null ? product.Price : productToUpdate.Price;
            repository.Update(id, productToUpdate);
            return TypedResults.Created($"{id}", productToUpdate);
        }

        public static async Task<IResult> DeleteProduct(IRepository<Product> repository, int id)
        {
            if (!IsAnExistingId(repository, id)) return Results.NotFound("No products with the the provided ID could be found.");
            var deletedProduct = repository.Delete(id);
            return deletedProduct != null ? TypedResults.Ok(deletedProduct) : Results.NotFound();
        }

        private static bool IsAnExistingName(IRepository<Product> repository, string name)
        {
            if (name == null || name == "") return false;
            return repository.GetAll().Any(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        private static bool IsAnExistingId(IRepository<Product> repository, int id)
        {
            return repository.GetAll().Any(x => x.Id == id);
        }

    }
}
