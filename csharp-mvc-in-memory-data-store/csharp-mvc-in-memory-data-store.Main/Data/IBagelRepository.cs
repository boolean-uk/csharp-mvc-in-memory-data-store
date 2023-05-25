using mvc_in_memory_data_store.Models;

namespace mvc_in_memory_data_store.Data
{
    public interface IBagelRepository
    {
        Bagel Create(string bagelType, int price);       
        List<Bagel> FindAll();     
        Bagel Find(int id);
        bool Add(Bagel bagel);
        bool Delete(int id);
        Bagel Update(int id, string? bagelType, int? price);
    }
}
