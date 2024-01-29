using exercise.wwwapi.Model;

namespace exercise.wwwapi.Repository
{
    public interface IDiscountRepository
    {
        Task<IEnumerable<Discount>> GetDiscounts();
        Task<Discount> GetDiscount(int id);
        Task<Discount> AddDiscount(Discount discount);
        Task UpdateDiscount(int id , Discount discount);
        Task<Discount> DeleteDiscount(int id);
    }
}
