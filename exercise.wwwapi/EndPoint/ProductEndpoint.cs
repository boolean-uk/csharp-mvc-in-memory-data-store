using exercise.wwwapi.Model;
using exercise.wwwapi.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace exercise.wwwapi.EndPoint
{
    public static class ProductEndpoint
    {
        public static void ConfigureProductEndpoint(this WebApplication app)
        {
            var ProductGroup = app.MapGroup("products");

            ProductGroup.MapGet("/", GetAll);
            ProductGroup.MapGet("/{id}", Get);
            ProductGroup.MapPost("/", Post);
            ProductGroup.MapPut("/{id}", Put);
            ProductGroup.MapDelete("/{id}", Delete);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetAll(IProductRepository repository)
        {
            return TypedResults.Ok(repository.Get());
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> Get(IProductRepository repository, int id)
        {
            return TypedResults.Ok(repository.Get(id));
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        public static async Task<IResult> Post(IProductRepository repository, Product product)
        {
            return TypedResults.Created("url", repository.Create(product));
        }

        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public static async Task<IResult> Put(IProductRepository repository, int id, Product product)
        {
            return TypedResults.Accepted("url", repository.Update(id, product));
        }

        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public static async Task<IResult> Delete(IProductRepository repository, int id)
        {
            return TypedResults.Accepted("url", repository.Delete(id));
        }
    }
}
