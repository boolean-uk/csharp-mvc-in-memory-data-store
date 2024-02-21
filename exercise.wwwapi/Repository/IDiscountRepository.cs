using exercise.wwwapi.Model;

namespace exercise.wwwapi.Repository
{
    public interface IDiscountRepository
    {
        public Task<List<Discount>> GetAllAsync();
        public Task<Discount?> GetByIdAsync(int id);
        public Task<Discount> AddAsync(DiscountCreatePayload payload);
        public Task<Discount?> UpdateAsync(int id, DiscountUpdatePayload discountData);
        public Task<bool> DeleteAsync(int id);
    }
}
