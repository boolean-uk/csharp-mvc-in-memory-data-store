

using exercise.wwwapi.Models.Discounts;
using System.ComponentModel.DataAnnotations.Schema;

namespace exercise.wwwapi.Models.Products
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int Price { get; set; }

        public List<Discount> Discounts { get; set; }

    }
}
