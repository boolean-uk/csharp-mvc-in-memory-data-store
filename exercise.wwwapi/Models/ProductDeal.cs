using System.ComponentModel.DataAnnotations.Schema;

namespace exercise.wwwapi.Models
{
    public class ProductDeal
    {
        public int Id { get; set; }
        public string Description { get; set; }

        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
    }
}
