namespace mvc_in_memory_data_store.Models
{
    public class Bagel
    {
        private int id;
        private String type;
        private int price;

        public Bagel(int id, String type, int price)
        {
            this.id = id;
            this.type = type;
            this.price = price;
        }

        public int GetId()
        {
            return this.id;
        }

        public String GetType()
        {
            return this.type;
        }

        public int GetPrice()
        {
            return this.price;
        }
    }
}
