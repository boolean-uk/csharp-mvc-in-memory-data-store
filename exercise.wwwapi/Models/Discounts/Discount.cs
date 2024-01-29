using exercise.wwwapi.Models.Products;
using System.ComponentModel.DataAnnotations.Schema;

namespace exercise.wwwapi.Models.Discounts
{
    public class Discount
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public decimal Percentage { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
     

    }
}

