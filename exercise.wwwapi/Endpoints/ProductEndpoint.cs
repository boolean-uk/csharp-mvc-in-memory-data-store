using System.Diagnostics.Contracts;
using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;
using exercise.wwwapi.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoint
    {
        
        public static void ConfigureEndpoint(this WebApplication app)
        {
            var products = app.MapGroup("products");

            products.MapGet("/{id}", Get);
            products.MapGet("/{category}", GetAll);
            products.MapPost("/", Add);
            products.MapPut("/{id}", Update);
            products.MapDelete("/{id}", Remove);
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> Get(IRepository repo ,int id)
        {
            try
            {
                var result = await repo.Get(id);
                return TypedResults.Ok(result);
            }
            catch (Exception ex)
            {
                return TypedResults.Problem(ex.Message);
            }
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetAll(IRepository repo, string category="" )
        {
            try
            {
                var result = await repo.GetAll();
                return TypedResults.Ok(result);
            }
            catch (Exception ex)
            {
                return TypedResults.Problem(ex.Message);
            }

        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> Add(IRepository repo, ProductPost entity)
        {
            try
            {
                Product newProduct = new Product();
                newProduct.price = entity.price;
                newProduct.name = entity.name;
                newProduct.category = entity.category;
                await repo.Add(newProduct);
                return TypedResults.Ok(newProduct);
            }
            catch (Exception e)
            {
                return TypedResults.Problem(e.Message);
            }

        }
        /// <summary>
        /// updates Product
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult>  Update(IRepository repo, int id ,ProductPut entity )
        {
            try 
            {
                var target = await repo.Get(id);
                if (target == null) return TypedResults.NotFound();
                if (entity.name != null) target.name = entity.name;
                if (entity.price!=null) target.price = entity.price;
                if (entity.category!=null) target.category = entity.category;
                return Results.Ok(target);
            }
            catch(Exception e)
            {
                return TypedResults.Problem(e.Message);

            }
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> Remove(IRepository repo, int id)
        {
            try
            {
                var target = await repo.Get(id);
                if (await repo.Delete(id)!=null) return Results.Ok(target);
                return TypedResults.NotFound();
            }
            catch(Exception e) 
            {
                return TypedResults.Problem(e.Message);
            }
        }
    }
}
