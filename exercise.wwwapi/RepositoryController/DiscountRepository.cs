using wwwapi.Data;
using wwwapi.Models;
using Microsoft.EntityFrameworkCore;
using exercise.wwwapi.Model;

namespace wwwapi.Repository
{
    public class DiscountRepository : IRepository<Discount, DiscountUpdatePayload>
    {
        private ProductContext _db;

        public DiscountRepository(ProductContext db)
        {
            _db = db;
        }

        public async Task<List<Discount>> GetAll()
        {
            return await _db.Discounts.Include(d => d.Products).ToListAsync();
        }

        public async Task<Discount> Add(Discount discount)
        {
            await _db.Discounts.AddAsync(discount);
            await _db.SaveChangesAsync();
            return discount;
        }

        public async Task<bool> Delete(int id)
        {
            Discount? discount = await Get(id);
            if (discount == null) { return false; }
            _db.Discounts.Remove(discount);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<Discount?> Get(int id)
        {
            Discount? discount = await _db.Discounts
                .Include(d => d.Products)
                .FirstOrDefaultAsync(d => d.Id == id);

            return discount;
        }

        public async Task<Discount> Update(int id, DiscountUpdatePayload payload)
        {
            Discount? discount = await Get(id);
            if (discount == null) { return null; }

            if (payload.percentage != null) { discount.percentage = payload.percentage.Value; }

            // Add any other fields in DiscountUpdatePayload and update them as needed

            await _db.SaveChangesAsync();
            return discount;
        }
    }
}
