using System.ComponentModel.DataAnnotations;

namespace exercise.wwwapi.ViewModel
{
    public class ProductViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string name { get; set; }
        [Required(ErrorMessage = "Price is required")]
        public int price { get; set; }
    }
}
