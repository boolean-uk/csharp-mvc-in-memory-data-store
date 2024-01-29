using System.ComponentModel.DataAnnotations.Schema;

namespace exercise.wwwapi.Models
{
    public class Discount
    {
        public int Id { get; set; }
        public int PriceOff { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
    }
}
