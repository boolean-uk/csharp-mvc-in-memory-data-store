using System.ComponentModel.DataAnnotations;

namespace exercise.wwwapi.Model
{
    public class ProductPostPayload
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; init; }

        [Required(ErrorMessage = "Category is required")]
        public string Category { get; init; }

        [Required(ErrorMessage = "Price is required"), Range(0, int.MaxValue, ErrorMessage = "Value must be a number!")]
        public int Price { get; init; }

        public ProductPostPayload(string name, string category, int price) 
        {
            Name = name;
            Category = category;
            Price = price;
        }
    }
}
