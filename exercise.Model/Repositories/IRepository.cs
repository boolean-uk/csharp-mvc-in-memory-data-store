using exercise.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exercise.Model.Repositories
{
    public interface IRepository<T> where T : DatabaseItem
    {
        /// <summary>
        /// Get all items from the context
        /// </summary>
        /// <returns>An ICollection of all items of type T</returns>
        public Task<ICollection<T>> GetAll();
        /// <summary>
        /// Gets a database item with the given id
        /// </summary>
        /// <param name="id">The id of the item</param>
        /// <returns>An item of type T</returns>
        public Task<T> GetById(Guid id);
        /// <summary>
        /// Deletes a database item with the given id
        /// </summary>
        /// <param name="id">The id of the item</param>
        /// <returns>The deleted item of type T</returns>
        public Task<T> DeleteById(Guid id);
        /// <summary>
        /// Add a database item 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>The added item</returns>
        public Task<T> Add(T item);
        /// <summary>
        /// Updates an existing database item by id
        /// </summary>
        /// <param name="id">The id of the item to change</param>
        /// <param name="item">The new properties of the item</param>
        /// <returns>The updated database item</returns>
        public Task<T> Update(Guid id, T item);
    }
}
