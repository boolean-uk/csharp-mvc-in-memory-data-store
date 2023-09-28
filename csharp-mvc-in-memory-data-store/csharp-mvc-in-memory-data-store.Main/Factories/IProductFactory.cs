using mvc_in_memory_data_store.DTOs;
using mvc_in_memory_data_store.Models;

namespace mvc_in_memory_data_store.Factories
{
    public interface IProductFactory
    {
        Product CreateProduct(ProductInput productInput);
    }
}
