using exercise.wwwapi.Repository;

namespace exercise.wwwapi.Endpoints
{
    public static class DiscountEndpoints
    {
        public static void ConfigureDiscountEndpoints(this WebApplication app)
        {
            var productGroup = app.MapGroup("discount");
            productGroup.MapPost("/{Productid}/{DiscountPercentage}", AttachDiscount);
            productGroup.MapDelete("/{Productid}", DetachDiscount);
        }

        public async static Task<IResult> AttachDiscount(DiscountRepository discount, int Productid, int DiscountPercentage)
        {
            try
            {
                return TypedResults.Ok(await discount.AttachDiscount(Productid, DiscountPercentage));
            }
            catch
            {
                return TypedResults.NotFound("Product doesn't exist");
            } 
        }
        public async static Task<IResult> DetachDiscount(DiscountRepository discount, int Productid)
        {
            try
            {
                return TypedResults.Ok(await discount.DetachDiscount(Productid));
            }
            catch
            {
                return TypedResults.NotFound("Product doesn't exist");
            }
        }
    }
}
