using Microsoft.AspNetCore.Mvc;
using exercise.wwwapi.Model;
using exercise.wwwapi.Repository;
using Microsoft.OpenApi.Writers;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;
using System;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;

namespace exercise.wwwapi.Endpoints
{

    public static class DiscountEndpoints
    {
        public static void ConfigureDiscountEndpoints(this WebApplication app)
        {

            var group2 = app.MapGroup("discounts");
            group2.MapGet("/", GetAllDiscounts);
            group2.MapGet("/{id}", GetDiscount);
            group2.MapPost("/", CreateDiscount);

            app.UseExceptionHandler(c => c.Run(async context =>
            {
                var exception = context.Features
                    .Get<IExceptionHandlerFeature>()
                    ?.Error;
                if (exception is not null)
                {
                    var response = new { error = exception.Message };
                    context.Response.StatusCode = 400;

                    await context.Response.WriteAsJsonAsync("Price must be an integer, something else was provided.");
                }
            }));

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetAllDiscounts(IDiscountRepository discounts)
        {

            List<Discount> res = await discounts.GetAllDiscounts();

            return TypedResults.Ok(res);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetDiscount(IDiscountRepository discounts, int amount)
        {
            Discount? prod = await discounts.GetDiscount(amount);

            if (prod == null)
            {
                return TypedResults.NotFound($"Discount not found.");
            }
            return TypedResults.Ok(prod);
        }

      

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> CreateDiscount(int amount, IDiscountRepository discounts) 
        {
           
            List<Discount> res = await discounts.GetAllDiscounts(); //.Exists(x => x.Name == newData.Name);
            bool alreadyExists = res.Exists(x => x.Amount == amount);

            if (alreadyExists)
            {
               return TypedResults.BadRequest("Discount with provided amount already exists.");
            }

            
            Discount dc = await discounts.AddDiscount(amount);

            return TypedResults.Created($"/discounts{dc.Id}", dc);
        } 

    }
}
