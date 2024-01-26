using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace exercise.wwwapi.Model.Models
{
    public class Product
    {
        private static int nextID;
        private int id;
        private string name;
        private string category;
        private int price;

        public Product(string name, string category, int price)
        {
            id = nextID++;
            this.name = name;
            this.category = category;
            this.price = price;
        }

        public string Name { get => name; set => name = value; }
        public string Category { get => category; set => category = value; }
        public int Price { get => price; set => price = value; }

        [Key]
        public int Id { get => id; set => id = value; }
    }
}
