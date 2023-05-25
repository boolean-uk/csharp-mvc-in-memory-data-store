using mvc_in_memory_data_store.Data;
using System;

namespace mvc_in_memory_data_store.Models
{
    public class BagelRepository : IBagelRepository
    {
        private static int IdCounter = 1;
        private static List<Bagel> _bagels = new List<Bagel>();

        public Bagel Create(string type, int price)
        {
            Bagel bagel = new Bagel(IdCounter++, type, price);
            _bagels.Add(bagel);
            return bagel;
        }

        public List<Bagel> FindAll()
        {
            return _bagels;
        }

        public Bagel Find(int id)
        {
            return _bagels.SingleOrDefault(x => x.Id == id);
        }

        public bool Add(Bagel bagel)
        {
            if (bagel != null)
            {
                bagel.Id = IdCounter;
                IdCounter++;
                _bagels.Add(bagel);
                return true;
            }
            return false;
        }

        public Bagel Update(int id, string? bagelType, int? price)
        {
            var bagel = _bagels.SingleOrDefault(x => x.Id == id);
            if (bagel != null)
            {
                if (!string.IsNullOrEmpty(bagelType))
                {
                    bagel.BagelType = bagelType;
                }
                if (!string.IsNullOrEmpty(price.ToString()))
                {
                    bagel.Price = (int)price;
                }
                return bagel;
            }
            else
            {
                return bagel;
            }
            
        }

        public bool Delete(int id)
        {
            var bagel = _bagels.SingleOrDefault(x => x.Id == id);
            if (bagel != null)
            {
                _bagels.Remove(bagel);
                return true;
            }
            return false;
        }
    }
}
