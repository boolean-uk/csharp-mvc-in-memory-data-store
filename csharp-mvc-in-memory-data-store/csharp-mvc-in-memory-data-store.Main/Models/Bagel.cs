namespace mvc_in_memory_data_store.Models
{
    public class Bagel
    {
        private int _id;
        private string _type;
        private int _price;

        public Bagel(int id, string type, int price)
        {
            this.id = id;
            this.type = type;
            this.price = price;
        }
        
        public int id { get { return _id; } set { _id = value; } }
        public string type { get { return _type; } set { _type = value; } }
        public int price { get { return _price; } set { _price = value; } }
    }
}
