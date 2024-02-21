using exercise.wwwapi.Model;
using exercise.wwwapi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Endpoints
{
    public static class DiscountEndpoints
    {
        public static void ConfigureDiscountEndpoints(this WebApplication app)
        {
            var discountGroup = app.MapGroup("/discounts");
            discountGroup.MapGet("", GetAllDiscounts);
            discountGroup.MapGet("/{id}", GetDiscountById);
            discountGroup.MapPost("", CreateDiscount);
            discountGroup.MapPut("/{id}", UpdateDiscount);
            discountGroup.MapDelete("/{id}", DeleteDiscount);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetAllDiscounts(IDiscountRepository discounts)
        {
            var results = await discounts.GetAll();
            return TypedResults.Ok(results);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetDiscountById(IDiscountRepository discounts, int id)
        {
            var result = await discounts.GetById(id);
            if (result == null)
            {
                return TypedResults.NotFound($"Discount with id {id} not found");
            }
            return TypedResults.Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> CreateDiscount(IDiscountRepository discounts, DiscountCreatePayload payload)
        {
            var discount = await discounts.Add(payload);
            return TypedResults.Created($"/discounts/{discount.Id}", discount);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> UpdateDiscount(IDiscountRepository discounts, int id, DiscountUpdatePayload payload)
        {
            Discount? discount = await discounts.Update(id, payload);
            if (discount == null)
            {
                return TypedResults.NotFound($"Discount with id {id} not found");
            }
            return TypedResults.Ok(discount);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> DeleteDiscount(IDiscountRepository discounts, int id)
        {
            bool deleted = await discounts.Delete(id);
            if (!deleted)
            {
                return TypedResults.NotFound($"Discount with id {id} not found");
            }
            return TypedResults.NoContent();
        }
    }
}
