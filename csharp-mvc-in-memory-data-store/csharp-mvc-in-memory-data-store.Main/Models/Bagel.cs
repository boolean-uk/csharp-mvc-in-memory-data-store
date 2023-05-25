using System.ComponentModel.DataAnnotations;

namespace mvc_in_memory_data_store.Models
{
    public class Bagel
    {
        private int id;
        private string bagelType;
        private int price;

        public Bagel(int id, string bagelType, int price)
        {
            this.id = id;
            this.bagelType = bagelType;
            this.price = price;
        }

        public int Id { get => this.id; set => this.id = value; }
        public string BagelType { get => this.bagelType; set => this.bagelType = value; }
        public int Price { get=> this.price; set => this.price = value; }
    }
}
