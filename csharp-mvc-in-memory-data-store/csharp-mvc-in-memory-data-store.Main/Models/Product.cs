using System.ComponentModel.DataAnnotations;

namespace mvc_in_memory_data_store.Models
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "This field is requared")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This field is requared")]
        public string Category { get; set; }

        [Required(ErrorMessage = "This field is requared")]
        public decimal Price { get; set; }

        //Default constructor
        public Product()
        {
            
        }

        //constructor with all parameters
        public Product(string name, string category, decimal price)
        {
            Name = name;
            Category = category; 
            Price = price;

        }
    }
}
