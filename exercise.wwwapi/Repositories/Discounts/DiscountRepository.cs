using exercise.wwwapi.Data;
using exercise.wwwapi.Models.Discounts;
using exercise.wwwapi.Models.Products;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace exercise.wwwapi.Repositories.Discounts
{
    public class DiscountRepository : IDiscountRepository
    {
        private DiscountContext _db;
        private int current_id = 0;
        public DiscountRepository(DiscountContext db) { _db = db; }



        public async Task<Discount> addDiscount(DiscountPostPayload payload)
        {
            try {
                var newDiscount = new Discount() { Id = getNextId(), Code = payload.code, Percentage = payload.percentage };
                _db.Add(newDiscount);
                await _db.SaveChangesAsync();
                return newDiscount;
            } catch (Exception e) {
                Console.WriteLine($"An unexpected error occurred: {e}");
                return null;
            }
        }

        private int getNextId()
        {
            return current_id++;
        }

        public async Task<bool> deleteDiscount(int _id)
        {
            var result = await getDiscountById(_id);
            if (result == null)
            {
                return false;
            }
            _db._discounts.Remove(result);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<Discount>> getAllDiscounts()
        {
            var result = await _db._discounts.ToListAsync();
            return result;
        }

        public async Task<Discount?> getDiscountById(int _id)
        {
            var result = await _db._discounts.FirstOrDefaultAsync(d => d.Id == _id);
            return result;
        }
    }
}
