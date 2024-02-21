using System.ComponentModel.DataAnnotations.Schema;

namespace exercise.wwwapi.Model
{
    public class Discount
    {
        public int Id { get; set; }

        public string? Code { get; set; }

        public double? Percentage { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();


    }
}
