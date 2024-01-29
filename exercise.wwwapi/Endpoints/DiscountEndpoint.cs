
using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Endpoints {

    public static class DiscountEndpoint {

        public static void ConfigureDiscountEndpoints(this WebApplication app) {
            var discounts = app.MapGroup("discounts");
            discounts.MapPost("/", CreateDiscount);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> CreateDiscount(IProductRepository sr, int Price)
        {
            Discount discount = await sr.AddDiscount(Price);
            return TypedResults.Created($"/discounts/{discount.Id}", discount);
        }
    }
}