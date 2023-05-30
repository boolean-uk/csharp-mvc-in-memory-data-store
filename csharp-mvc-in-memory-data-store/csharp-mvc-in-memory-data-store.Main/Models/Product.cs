using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace mvc_in_memory_data_store.Models
{
    public class Product
    {
        public int id { get; set; }

        [Required(ErrorMessage ="Name is a required field", AllowEmptyStrings =false)]
        [Description("Name of the product")]

        public string? Name { get; set; }

        [Required(ErrorMessage = "Category is a required field", AllowEmptyStrings = false)]
        [Description("Category of the product")]
        public string? Category { get; set; }

        [Required(ErrorMessage = "Price is a required field", AllowEmptyStrings = false)]
        [Description("Price of the product")]
        public decimal Price { get; set; }
        
    }
}
