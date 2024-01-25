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
    public class ProductRepository : Repository<Product>
    {
        public ProductRepository(DataContext<Product> dbContext) : base(dbContext)
        {
        }

        private async Task<bool> NameExistsInDatabase(string name)
        {
            return await _dbContext.Data.AnyAsync(x => x.Name.Equals(name));
        }

        public async Task<ICollection<Product>> GetAll(string category)
        {
            if (category != null) 
            {
                return await _dbContext.Data.Where(x => x.Category.ToLower() == category.ToLower()).ToListAsync();
            }
            return await _dbContext.Data.ToListAsync();
        }

        public override async Task<Product> Add(Product item)
        {
            if (await NameExistsInDatabase(item.Name))
            {
                throw new ArgumentException($"Product with name '{item.Name}' already exists!");
            }
            return await base.Add(item);
        }

        public override async Task<Product> Update(Guid id, Product newItem)
        {
            if (await NameExistsInDatabase(newItem.Name))
            {
                throw new ArgumentException($"Product with name '{newItem.Name}' already exists!");
            }
            return await base.Update(id, newItem);
        }
    }
}
