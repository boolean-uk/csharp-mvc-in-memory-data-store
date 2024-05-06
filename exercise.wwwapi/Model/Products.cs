using System.ComponentModel.DataAnnotations;

namespace exercise.wwwapi.Model
{
    public class Products
    {
        public int Id { get; set; }

        //[Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        //[Required(ErrorMessage = "Category is required")]
        public string Category { get; set; }

       // [Required(ErrorMessage = "Price is required")]
        public int Price { get; set; }
    }
}
