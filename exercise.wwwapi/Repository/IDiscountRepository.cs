using exercise.wwwapi.Model;

namespace exercise.wwwapi.Repository
{
    public interface IDiscountRepository
    {
        public Task<List<Discount>> GetAll();
        public Task<Discount?> GetById(int id);
        public Task<Discount> Add(DiscountCreatePayload payload);
        public Task<Discount?> Update(int id, DiscountUpdatePayload discountData);
        public Task<bool> Delete(int id);
    }
}
