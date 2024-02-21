using exercise.wwwapi.Data;
using exercise.wwwapi.Model;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Repository
{
    public class DiscountRepository: IDiscountRepository
    {
        private DataContext _db;

        public DiscountRepository(DataContext db)
        {
            _db = db;
        }

        public async Task<List<Discount>> GetAllAsync()
        {
            var discounts = await _db.Discounts.ToListAsync();
            return discounts;
        }

        public async Task<Discount?> GetByIdAsync(int id)
        {
            var discount = await _db.Discounts.FirstOrDefaultAsync(d => d.Id == id);
            return discount;
        }

        public async Task<Discount> AddAsync(DiscountCreatePayload payload)
        {
            var discount = new Discount() { Code = payload.Code, Percentage = payload.Percentage };
            await _db.AddAsync(discount);
            await _db.SaveChangesAsync();
            return discount;
        }

        public async Task<Discount?> UpdateAsync(int id, DiscountUpdatePayload discountData)
        {
            var discount = await GetByIdAsync(id);
            if (discount == null)
            {
                return null;
            }

            if (discountData.Code != null)
            {
                discount.Code = (string)discountData.Code;
            }

            if (discountData.Percentage != null)
            {
                discount.Percentage = (double)discountData.Percentage;
            }

            await _db.SaveChangesAsync();
            return discount;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var discount = await GetByIdAsync(id);
            if (discount == null)
            {
                return false;
            }
            _db.Remove(discount);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
