using exercise.wwwapi.Data;
using exercise.wwwapi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace exercise.wwwapi.Repository
{
    public class DiscountRepository : IDiscountRepository
    {
        public ProductContext _db;

        public DiscountRepository(ProductContext db)
        {
            _db = db;
        }

        public async Task<List<Discount>> GetAllDiscounts()
        {

            return await _db.Discounts.ToListAsync();
        }

        public async Task<Discount> AddDiscount(int amount, int id)
        {
            Discount disc2 = new Discount { Amount = amount, ProductId = id };

            _db.Discounts.Add(disc2); //.Add(disc);
            _db.SaveChanges(); //.SaveChanges();

            return disc2;
        }

        public async Task<Discount?> GetDiscount(int amount)
        {
            return await _db.Discounts.FirstOrDefaultAsync(x => x.Amount == amount);
        }

    }
}
