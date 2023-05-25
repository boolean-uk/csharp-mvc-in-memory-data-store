using System.ComponentModel.DataAnnotations;

namespace mvc_in_memory_data_store.Models
{
    public class Bagel
    {
        
        private int id;
        private string type;
        private int price;

        public Bagel(int id, String type, int price)
        {
            this.id = id;
            this.type = type;
            this.price = price;
        }
        [Required]
        public int Id { get { return id; } set { id = value; } }
        [Required]
        public string Type { get { return type; } set { type = value; } }
        [Required]
        public int Price { get { return price;} set { price = value; } }
    
    }
}
