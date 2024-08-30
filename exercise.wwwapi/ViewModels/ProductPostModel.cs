using System.ComponentModel.DataAnnotations;

namespace exercise.wwwapi.ViewModels
{
    public class ProductPostModel
    {

        [Required(ErrorMessage = "Product with provided name already exist")]
        public string Name {  get; set; }

        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; }
    
        [Required(ErrorMessage = "Price must be an interger, something else was provided")]
        public int Price {  get; set; }
    }
}
