

using System.ComponentModel.DataAnnotations.Schema;

namespace exercise.wwwapi.Models {

    public class Product {

        public int Id {get; set;}
        public required string Name {get; set;}
        public required string Category {get; set;}
        public int Price {get; set;}
        public int? DiscountId {get; set; }
        public Discount? Discount {get; set;}

    }
}
