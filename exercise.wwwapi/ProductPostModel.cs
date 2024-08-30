using System.ComponentModel.DataAnnotations;

namespace exercise.wwwapi
{
    public class ProductPostModel
    {
        [Required]
        public string? name { get; set; }
        [Required]
        public string? category { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Price must be an integer, something else was provided.")]
        public int price { get; set; }
    }
}
