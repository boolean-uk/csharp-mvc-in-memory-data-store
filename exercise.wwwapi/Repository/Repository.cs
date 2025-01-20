using exercise.wwwapi.Data;
using exercise.wwwapi.Models;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Repository
{
    public class Repository : IRepository
    {
        private DataContext _db;

        public Repository(DataContext db)
        {
            _db = db;
        }
        public async Task<Products> AddProduct(Products entity)
        {
            await _db.ProductDB.AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity;

        }

        public async Task<bool> DeleteProduct(int id)
        {
            var target = await _db.ProductDB.FindAsync(id);
            _db.ProductDB.Remove(target);
            await _db.SaveChangesAsync();
            return true;


        }

        public async Task<Products> GetProduct(int id)
        {
            return await _db.ProductDB.FindAsync(id);
        }

        public async Task<IEnumerable<Products>> GetProducts()
        {
            return await _db.ProductDB.ToListAsync();
        }

        public async Task<IEnumerable<Products>> GetProducts(string category)
        {
            var result = await _db.ProductDB.ToListAsync();
            result = result.FindAll(p => p.Category.ToLower() == category.ToLower());
            return result;
        }

        public async Task<Products> UpdateProduct(Products entity)
        {
            var target = await _db.ProductDB.FindAsync(entity.Id);
            _db.ProductDB.Remove(target);
            await _db.SaveChangesAsync();

            await _db.ProductDB.AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity;

        }

    }
}
