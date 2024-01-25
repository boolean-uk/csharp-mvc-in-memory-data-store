using exercise.wwwapi.Data;
using exercise.wwwapi.Models;
using exercise.wwwapi.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Repositories
{
    public interface IProductRepository
    {
        Task<Product?> CreateProductAsync(CreateProductDTO createDTO);
        Task<List<Product>> GetAllProductsAsync(string category);
        Task<Product?> GetProductByIdAsync(int id);
        Task<Tuple<Product?, int>> UpdateProductByIdAsync(int id, CreateProductDTO updateDTO);
        Task<Product> DeleteProductByIdAsync(int id);
    }
    public class ProductRepository : IProductRepository
    {
        private readonly IDataContext _context;

        public ProductRepository(IDataContext context)
        {
            _context = context;
        }

        public async Task<Product?> CreateProductAsync(CreateProductDTO createDTO)
        {
            Product? dbProductWithName = await _context.Products.Where(x => x.Name == createDTO.Name).FirstOrDefaultAsync();
            if (dbProductWithName != null)
            {
                return null;
            }
            Product p = new() { Name = createDTO.Name, Category = createDTO.Category, Price = Int32.Parse(createDTO.Price) };
            _context.Products.Add(p);
            await _context.SaveChangesAsync();
            return p;
        }

        public async Task<List<Product>> GetAllProductsAsync(string category)
        {
            if (category.Length > 0)
            {
                return await _context.Products.Where(x => x.Category == category).ToListAsync();
            }
            else
            {
                return await _context.Products.ToListAsync();
            }
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Tuple<Product?, int>> UpdateProductByIdAsync(int id, CreateProductDTO updateDTO)
        {
            Product? dbProductWithName = await _context.Products.Where(x => x.Name == updateDTO.Name).FirstOrDefaultAsync();
            if (dbProductWithName != null)
            {
                return new(null, -1);
            }
            Product? dbProduct = await GetProductByIdAsync(id);
            if (dbProduct == null)
            {
                return new(null, -2);
            }
            dbProduct.Name = updateDTO.Name;
            dbProduct.Category = updateDTO.Category;
            dbProduct.Price = Int32.Parse(updateDTO.Price);
            _context.Products.Update(dbProduct);
            await _context.SaveChangesAsync();
            return new(dbProduct, 0);
        }

        public async Task<Product> DeleteProductByIdAsync(int id)
        {
            Product dbProduct = await GetProductByIdAsync(id);
            if (dbProduct == null)
            {
                return null;
            }
            _context.Products.Remove(dbProduct);
            await _context.SaveChangesAsync();
            return dbProduct;
        }
    }
}
