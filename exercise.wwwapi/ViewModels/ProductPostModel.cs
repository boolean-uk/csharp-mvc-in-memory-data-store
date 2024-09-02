using System.ComponentModel.DataAnnotations;

namespace exercise.wwwapi.ViewModels
{
    public class ProductPostModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string name { get; set; }
        [Required(ErrorMessage = "Category is required")]
        public string category { get; set;}
        [Required(ErrorMessage = "Price is required")]
        public int price { get; set; }
    }
}
