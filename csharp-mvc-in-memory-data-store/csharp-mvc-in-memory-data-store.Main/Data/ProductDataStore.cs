using mvc_in_memory_data_store.Models;

namespace mvc_in_memory_data_store.Data
{
    public class ProductDataStore
    {
        public static List<Product> Products { get; set; } = new List<Product>();
    }
}
