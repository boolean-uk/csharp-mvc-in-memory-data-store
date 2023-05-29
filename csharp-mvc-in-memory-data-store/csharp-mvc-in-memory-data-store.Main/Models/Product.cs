using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace mvc_in_memory_data_store.Models
{
    public class Product
    {
        public int _id { get; set; }

        [Required(ErrorMessage = "Name of product is required", AllowEmptyStrings = false)]
        [Description("Product Name")]
        public string? _name { get; set; }

        [Required(ErrorMessage = "Category of product is required", AllowEmptyStrings = false)]
        [Description("Product Category")]
        public string? _category { get; set; }

        [Required(ErrorMessage = "Price of product is required", AllowEmptyStrings = false)]
        [Description("Product Category")]
        public decimal? _price { get; set; }
    }
}
