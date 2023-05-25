using mvc_in_memory_data_store.Data;
using System;

namespace mvc_in_memory_data_store.Models
{
    public class BagelRepository : IBagelRepository
    {
        private static int IdCounter = 1;
        private static List<Bagel> _bagels = new List<Bagel>();

        public int counterID { get { return IdCounter++; } }
        public BagelRepository()
        {
            if(_bagels.Count == 0)
            {
            Bagel bagel = new Bagel(BagelRepository.IdCounter, "plain", 10);

            _bagels.Add(bagel);

            }
        }

        public Bagel create(string type, int price)
        {
            int id = _bagels.Count;
            Bagel bagel = new Bagel(id++, type, price);
            BagelRepository._bagels.Add(bagel);
            return bagel;
        }

        public List<Bagel> findAll()
        {
            return BagelRepository._bagels;
            
        }

        public Bagel find(int id)
        {
            return BagelRepository._bagels.First(bagel => bagel.Id == id);
        }

        public IEnumerable<Bagel> DeleteBagel(int id)
        {
            var bagel = find(id);
            _bagels.Remove(bagel);
            return BagelRepository._bagels;
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
       public Bagel UpdateBagel(int id, Bagel b)
        {
            if (_bagels.Count == 0)
            {
                return null;
            }
            else
            {
                foreach (Bagel bagel in _bagels)
                {
                    if (bagel.Id.Equals(id))
                    {
                        bagel.Price = b.Price;
                        bagel.Type = b.Type;
                        return bagel;
                    }
                }
                return null;
            }

        }


    }
}
