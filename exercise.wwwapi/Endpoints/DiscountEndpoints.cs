
using exercise.wwwapi.Models.Discounts;
using exercise.wwwapi.Repositories.Discounts;

namespace exercise.wwwapi.Endpoints
{
    public static class DiscountEndpoints
    {

        public static void ConfigureDiscountEndpoints(this WebApplication app)
        {
            // endpoints
            var discountGroup = app.MapGroup("/discount");
            discountGroup.MapPost("/", AddDiscount);
            discountGroup.MapGet("/", GetAllDiscounts);
        }

        private static async Task<IResult> GetAllDiscounts(IDiscountRepository discount)
        {
            List<Discount> discounts = await discount.getAllDiscounts();
            return discounts.Count > 0
                ? TypedResults.Ok(discounts)
                : TypedResults.NotFound("No Discounts could be found");
        }

        private static async Task<IResult> AddDiscount(IDiscountRepository discount, DiscountPostPayload payload)
        {
            try
            {
                var result = await discount.addDiscount(payload);
                return result != null
                    ? TypedResults.Created($"/discounts", result)
                    : TypedResults.BadRequest("Invalid payload");
            }
            catch (Exception ex)
            {
                return TypedResults.BadRequest(ex.Message);
            }
            
        }
    }
}
