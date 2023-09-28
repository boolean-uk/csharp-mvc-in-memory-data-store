using System.ComponentModel.DataAnnotations;

namespace mvc_in_memory_data_store.DTOs
{
    public class ProductInput // added dto for input validation
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        public int Price { get; set; }
    }
}
