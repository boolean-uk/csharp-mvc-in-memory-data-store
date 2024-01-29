using exercise.wwwapi.Data;
using exercise.wwwapi.Model;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Repository
{
    public class DiscountRepository : IDiscountRepository
    {

        private readonly ProductContext _context;

        public DiscountRepository(ProductContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Discount>> GetDiscounts()
        {
            return await _context.Discounts.ToListAsync();
        }

        public async Task<Discount> GetDiscount(int id)
        {
            return await _context.Discounts.FindAsync(id);
        }

        public async Task<Discount> AddDiscount(Discount discount)
        {
            _context.Discounts.Add(discount);
            await _context.SaveChangesAsync();
            return discount;
        }

        public async Task UpdateDiscount(int id , Discount discount)
        {
            var existingDiscount = await _context.Discounts.FindAsync(id);
            if(existingDiscount == null)
            {
                return;
            }
            existingDiscount.Code = discount.Code;
            existingDiscount.Percentage = discount.Percentage;
            await _context.SaveChangesAsync();
        }

        public async Task<Discount> DeleteDiscount(int id)
        {
            var discount = await _context.Discounts.FindAsync(id);
            if(discount == null)
            {
                return null;
            }

            _context.Discounts.Remove(discount);
            await _context.SaveChangesAsync();
            return discount;
        }
    }
}
