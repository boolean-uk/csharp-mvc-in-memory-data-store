using System.ComponentModel.DataAnnotations;

namespace mvc_in_memory_data_store.Models
{
    public class Product // added validation
    {
        [Required(ErrorMessage = "Id is required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        public int Price { get; set; }
    }
}
