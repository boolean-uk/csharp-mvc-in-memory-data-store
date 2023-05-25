using System.ComponentModel.DataAnnotations;

namespace mvc_in_memory_data_store.Models.Requests
{
    public class ProductRequest
    {
        [Required(ErrorMessage = "This field is requared")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This field is requared")]
        public string Category { get; set; }

        [Required(ErrorMessage = "This field is requared")]
        public decimal Price { get; set; }
    }
}
