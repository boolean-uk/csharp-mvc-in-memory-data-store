using exercise.wwwapi.Data;
using exercise.wwwapi.Model;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Repository
{
    public class DiscountRepository
    {
        private ProductContext _db;
        public DiscountRepository(ProductContext db)
        {
            _db = db;
        }
        public async Task<Product> AttachDiscount(int idOfProduct, int discountPercentage)
        {
            Product? product = await _db.products.FirstOrDefaultAsync(x => x.id == idOfProduct);
            if (product is null) throw new Exception();
            Discount discount = new Discount() { percentage = discountPercentage, ProductId = idOfProduct };
            product.discount = discount;
            return product;
        }

        public async Task<Product> DetachDiscount(int ProductId)
        {
            Product? product = await _db.products.FirstOrDefaultAsync(x => x.id == ProductId);
            if (product is null) throw new Exception();
            product.discount = null;
            return product;
        }

    }
}
