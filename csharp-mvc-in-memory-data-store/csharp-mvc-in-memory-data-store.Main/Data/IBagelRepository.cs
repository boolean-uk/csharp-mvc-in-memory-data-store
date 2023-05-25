using mvc_in_memory_data_store.Models;

namespace mvc_in_memory_data_store.Data
{
    public interface IBagelRepository
    {
        Bagel create(string type, int price);       
        List<Bagel> findAll();     
        Bagel find(int id);
        bool Add(Bagel bagel);

        IEnumerable<Bagel> DeleteBagel(int id);

        Bagel UpdateBagel(int id,Bagel bagel);
    }
}
