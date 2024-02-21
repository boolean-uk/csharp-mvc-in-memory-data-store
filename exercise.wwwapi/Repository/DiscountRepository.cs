using exercise.wwwapi.Data;
using exercise.wwwapi.Model;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Repository
{
    public class DiscountRepository: IDiscountRepository
    {
        private DiscountContext _db;

        public DiscountRepository(DiscountContext db)
        {
            _db = db;
        }

        public async Task<List<Discount>> GetAll()
        {
            var discounts = await _db.Discounts.ToListAsync();
            return discounts;
        }

        public async Task<Discount?> GetById(int id)
        {
            var discount = await _db.Discounts.FirstOrDefaultAsync(d => d.Id == id);
            return discount;
        }

        public async Task<Discount> Add(DiscountCreatePayload payload)
        {
            var discount = new Discount() { Code = payload.Code, Percentage = payload.Percentage };
            await _db.AddAsync(discount);
            await _db.SaveChangesAsync();
            return discount;
        }

        public async Task<Discount?> Update(int id, DiscountUpdatePayload discountData)
        {
            var discount = await GetById(id);
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

            if (discountData.ProductId != null)
            {
                discount.ProductId = (int)discountData.ProductId;
            }

            await _db.SaveChangesAsync();
            return discount;
        }

        public async Task<bool> Delete(int id)
        {
            var discount = await GetById(id);
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
