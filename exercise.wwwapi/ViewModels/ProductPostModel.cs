using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace exercise.wwwapi.ViewModels
{
    public class ProductPostModel
    {
        
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Price is required")]

        public int Price { get; set; }        
    }
}
