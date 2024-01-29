using exercise.wwwapi.Models.Discounts;

namespace exercise.wwwapi.Repositories.Discounts
{
    public interface IDiscountRepository
    {
        public Task<List<Discount>> getAllDiscounts();
        public Task<Discount?> getDiscountById(int _id);
        public Task<Discount> addDiscount(DiscountPostPayload payload);
        public Task<bool> deleteDiscount(int _id);
    }
}
