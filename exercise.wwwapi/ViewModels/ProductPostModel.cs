using System.ComponentModel.DataAnnotations;

namespace exercise.wwwapi.ViewModels
{
    public class ProductPostModel
    {
        [Required(ErrorMessage = "Make is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Model is required")]
        public string Category { get; set; }
        public int Price { get; set; }
    }
}
