using mvc_in_memory_data_store.Data;
using System;

namespace mvc_in_memory_data_store.Models
{
    public class BagelRepository : IBagelRepository
    {
        private static int IdCounter = 1;
        private static List<Bagel> _bagels = new List<Bagel>();

        public Bagel create(string type, int price)
        {
            Bagel bagel = new Bagel(IdCounter++, type, price);
            _bagels.Add(bagel);
            return bagel;
        }

        public List<Bagel> findAll()
        {
            return _bagels;    
        }

        public Bagel find(int id)
        {
            return _bagels.First(bagel => bagel.id == id);
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

        public Bagel updateBagel(Bagel bagel)
        {
            foreach(Bagel b in _bagels)
            {
                if (b.id == bagel.id)
                {
                    b.type = bagel.type;
                    b.price = bagel.price;
                }
            }
            return bagel;
        }

        public bool Delete(int id)
        {
            foreach (Bagel b in _bagels)
            {
                if (b.id == id)
                {
                    _bagels.Remove(b);
                    return true;
                }
            }
            return false;
        }
    }
}
