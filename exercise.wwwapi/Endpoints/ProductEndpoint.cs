using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;
using exercise.wwwapi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoint
    {
        public static void ConfigureProductEndpoint(this WebApplication app)
        {
            var products = app.MapGroup("products");
            products.MapPost("/", Add);
            products.MapGet("/", GetAll);
            products.MapGet("/{id}", Get);
            products.MapPut("/{id}", Update);
            products.MapDelete("/{id}", Delete);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static IResult Add(IRepository<Product> repository, ProductPost view)
        {
            var products = repository.GetAll();
            if (products.FirstOrDefault(x => x.Name == view.Name) != null) return TypedResults.BadRequest(new ErrorModel());

            var product = new Product() { Name = view.Name, Category = view.Category, Price = view.Price };
            var result = repository.Add(product);
            return TypedResults.Created($"https://localhost:7188/products/", result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult GetAll(IRepository<Product> repository, string category="All")
        {
            if (category.Equals("All")) return TypedResults.Ok(repository.GetAll());

            var products = repository.GetAll(category);
            if (products.Count == 0) return TypedResults.NotFound(new ErrorModel());
            return TypedResults.Ok(products);

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult Get(IRepository<Product> repository, int id)
        {
            var result = repository.Get(id);
            return result != null ? TypedResults.Ok(result) : TypedResults.NotFound(new ErrorModel());
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult Update(IRepository<Product> repository, int id, ProductPost view)
        {
            var getResult = repository.Get(id);
            if (getResult == null) return TypedResults.NotFound(new ErrorModel());

            var products = repository.GetAll();
            if (products.FirstOrDefault(x => x.Name == view.Name) != null) return TypedResults.BadRequest(new ErrorModel());

            var product = new Product() { Name = view.Name, Category = view.Category, Price = view.Price };

            var result = repository.Update(id, product);
            return TypedResults.Created($"https://localhost:7188/products/{id}", result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult Delete(IRepository<Product> repository, int id)
        {
            var result = repository.Delete(id);
            return result != null ? TypedResults.Ok(result) : TypedResults.NotFound(new ErrorModel());
        }
    }
}
