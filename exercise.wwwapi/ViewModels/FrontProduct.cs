using System.ComponentModel.DataAnnotations;

namespace exercise.wwwapi.ViewModels
{
    public class FrontProduct
    {
        public string Name { get; set; }
        public string Category { get; set; }
        [Required]
        public int Price { get; set; }
    }
}
