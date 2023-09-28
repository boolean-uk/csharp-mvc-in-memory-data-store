using mvc_in_memory_data_store.DTOs;
using mvc_in_memory_data_store.Models;

namespace mvc_in_memory_data_store.Factories
{
    public class ProductFactory : IProductFactory
    {
        public Product CreateProduct(ProductInput productInput)
        {
            return new Product
            {
                Name = productInput.Name,
                Category = productInput.Category,
                Price = productInput.Price
            };
        }
    }
}
