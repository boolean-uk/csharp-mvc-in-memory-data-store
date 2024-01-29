using wwwapi.Data;
using wwwapi.Repository;
using wwwapi.Models;
using wwwapi.Repository;
using exercise.wwwapi.Model;

namespace wwwapi.EndPoints
{
    public static class DiscountEndPoints
    {
        public static void ConfigureDiscountEndPoints(this WebApplication app)
        {
            var discountGroup = app.MapGroup("discounts");
            discountGroup.MapPost("/", AddDiscount);
            discountGroup.MapGet("/", GetAllDiscounts);
            discountGroup.MapGet("/{id}", GetDiscountById);
            discountGroup.MapPut("/{id}", UpdateDiscount);
            discountGroup.MapDelete("/{id}", DeleteDiscountById);
        }

        public static async Task<IResult> AddDiscount(Discount discountPayload, IRepository<Discount, DiscountUpdatePayload> discountRepository)
        {
            Discount newDiscount = new Discount { percentage = discountPayload.percentage };
            // Add any additional mappings from DiscountPayload to Discount here
            await discountRepository.Add(newDiscount);
            return TypedResults.Ok(newDiscount);
        }

        public static async Task<IResult> GetAllDiscounts(IRepository<Discount, DiscountUpdatePayload> discountRepository)
        {
            return TypedResults.Ok(await discountRepository.GetAll());
        }

        public static async Task<IResult> GetDiscountById(int id, IRepository<Discount, DiscountUpdatePayload> discountRepository)
        {
            Discount? discount = await discountRepository.Get(id);
            if (discount == null) { return TypedResults.NotFound(); }
            return TypedResults.Ok(discount);
        }

        public static async Task<IResult> UpdateDiscount(int id, DiscountUpdatePayload discountUpdatePayload, IRepository<Discount, DiscountUpdatePayload> discountRepository)
        {
            Discount? discount = await discountRepository.Update(id, discountUpdatePayload);
            if (discount == null) { return TypedResults.NotFound(); }
            return TypedResults.Ok(discount);
        }

        public static async Task<IResult> DeleteDiscountById(int id, IRepository<Discount, DiscountUpdatePayload> discountRepository)
        {
            bool result = await discountRepository.Delete(id);
            if (result) { return TypedResults.Ok(); }
            return TypedResults.NotFound();
        }
    }
}
