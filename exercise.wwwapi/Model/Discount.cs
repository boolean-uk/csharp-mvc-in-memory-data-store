using System.ComponentModel.DataAnnotations.Schema;

namespace exercise.wwwapi.Model
{
    public class Discount
    {
        public int Id { get; set; }
        public int percentage { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
    }
}
