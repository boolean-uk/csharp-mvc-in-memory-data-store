using System.ComponentModel.DataAnnotations.Schema;

namespace exercise.wwwapi.Model
{
    public class Discount
    {
        public int Id { get; set; }

        public string? Code { get; set; }

        public double? Percentage { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
    }
}
