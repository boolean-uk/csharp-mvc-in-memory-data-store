using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public interface IRepository
    {
        /// <summary>
        /// Determine if the provided name is already located in the database.
        /// </summary>
        /// <param name="name"> string - the name of the <see cref="Product"/></param>
        /// <returns> bool - true if the name is available, false otherwise </returns>
        bool ProductNameIsAvailable(string? name);

        /// <summary>
        /// Post/Create a new <see cref="Product"/> and add to the database. 
        /// </summary>
        /// <param name="prod"> <see cref="ProductPost"/> - A prototype <see cref="Product"/> to be transcribed into a <see cref="Product"/> object</param>
        /// <returns> <see cref="Product"/> - The created <see cref="Product"/>, null if an error occured</returns>
        Product? PostProduct(ProductPost prod);

        /// <summary>
        /// Retrieve all <see cref="Product"/> within a specified category. Category can be empty, which would return all <see cref="Product"/> in the database.
        /// </summary>
        /// <param name="category"> string - Category to be returned. Can be null</param>
        /// <returns>IEnumerable - All <see cref="Product"/> with a category matching the provided category.</returns>
        IEnumerable<Product> GetProducts(string? category);

        /// <summary>
        /// Retrieve a specific <see cref="Product"/> from the database based on <see cref="Product"/>.Id
        /// </summary>
        /// <param name="id">int - the Id to be retrieved</param>
        /// <returns> <see cref="Product"/> - the <see cref="Product"/> that had the provided Id, null if no match found.</returns>
        Product? GetSpecificProduct(int id);

        /// <summary>
        /// Put/Update a <see cref="Product"/> in the database, based on provided Id.
        /// </summary>
        /// <param name="id"> int - The Id of the <see cref="Product"/> to be updated</param>
        /// <param name="prod"> <see cref="ProductPut"/> - the temporary object to be transcribed into a <see cref="Product"/></param>
        /// <returns> Tuple - item1 is a returned <see cref="Product"/>, item2 is error codes; either 201 (created), 400 (name already exists), or 404 (the product with provided id was not found).</returns>
        Tuple<Product?, int> PutProduct(int id, ProductPut prod);

        /// <summary>
        /// Attempt to delete a <see cref="Product"/> based on the <see cref="Product"/> Id.
        /// </summary>
        /// <param name="id"> int - the id of the <see cref="Product"/> to be deleted from the database</param>
        /// <returns><see cref="Product"/> - The deleted <see cref="Product"/> if found, null if <see cref="Product"/> was not found.</returns>
        Product? DeleteProduct(int id);

    }
}
