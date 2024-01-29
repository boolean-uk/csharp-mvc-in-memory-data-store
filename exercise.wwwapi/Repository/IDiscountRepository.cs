using exercise.wwwapi.Model;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Repository
{
   
    public interface IDiscountRepository
    {
        public Task<List<Discount>> GetAllDiscounts();

        public Task<Discount> AddDiscount(int amount);

        public Task<Discount?> GetDiscount(int amount);
    }
}
