using exercise.Model.Data;
using exercise.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exercise.Model.Repositories
{
    public class Repository<T> : IRepository<T> where T : DatabaseItem
    {
        protected readonly DataContext<T> _dbContext; 
        public Repository(DataContext<T> dbContext)
        {
            _dbContext = dbContext;
        }

        virtual public async Task<T> Add(T item)
        {
            await _dbContext.AddAsync(item);
            await _dbContext.SaveChangesAsync();
            return item;
        }

        public async Task<T> DeleteById(Guid id)
        {
            T item = await GetById(id);
            _dbContext.Remove(item);
            await _dbContext.SaveChangesAsync();
            return item;
        }

        public async Task<ICollection<T>> GetAll()
        {
            return await _dbContext.Data.ToListAsync();
        }

        public async Task<T> GetById(Guid id)
        {
            T item = await _dbContext.Data.FirstOrDefaultAsync(x => x.Id.Equals(id))
                ?? throw new Exception($"No item with id: {id}");
            return item;
        }

        virtual public async Task<T> Update(Guid id, T newItem)
        {
            T item = await GetById(id);
            item = newItem;
            await _dbContext.SaveChangesAsync();
            return item;
        }
    }
}
