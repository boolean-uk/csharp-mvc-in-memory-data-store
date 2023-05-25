using mvc_in_memory_data_store.Data;
using System;

namespace mvc_in_memory_data_store.Models
{
    public class BagelRepository : IBagelRepository
    {
        private static int IdCounter = 1;
        private static List<Bagel> _bagels = new List<Bagel>();

        public void create(String type, int price)
        {
            Bagel bagel = new Bagel(BagelRepository.IdCounter++, type, price);
            BagelRepository._bagels.Add(bagel);
        }

        public List<Bagel> findAll()
        {
            return BagelRepository._bagels;
            
        }

        public Bagel find(int id)
        {
            return BagelRepository._bagels.First(bagel => bagel.GetId() == id);
        }

        public bool Add(Bagel bagel)
        {
            if (bagel != null)
            {
                _bagels.Add(bagel);
                return true;
            }
            return false;
        }
    }
}
