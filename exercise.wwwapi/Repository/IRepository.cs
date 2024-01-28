using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> Get();

        /// <summary>
        /// Insert a new entity to the database. 
        /// </summary>
        /// <param name="entity"> Entity to be inserted into the database</param>
        /// <returns> <see cref="T"/> - The created entity.</returns>
        T Insert(T entity);

        /// <summary>
        /// Retrieve a specific entity from the database based on T.Id
        /// </summary>
        /// <param name="id">int - the Id of the entity to be retrieved.</param>
        /// <returns> T - the entity that had the provided Id, null if no match found.</returns>
        T? GetById(int id);

        /// <summary>
        /// Update an entity in the database.
        /// </summary>
        /// <param name="entity"> the entity object with changed information, entity must be a modified entity retrieved from the database</param>
        /// <returns> The updated entity object.</returns>
        T Update(T entity);

        /// <summary>
        /// Attempt to delete a entity object based on the database Id.
        /// </summary>
        /// <param name="id"> int - the id of the T entity to be deleted from the database</param>
        /// <returns>T object - The deleted entity if found, null if T object was not found.</returns>
        T? Delete(int id);

    }
}
