

using exercise.wwwapi.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace wwwapi.Models
{
    public class Product
    {
        private static int nProducts = 0;
        public string Name { get; set; }
        public string Category{ get; set; }
        public int Price { get; set; }

        public int id { get; init; }

        [ForeignKey("Discounts")]
        public int discountID { get; set; }
        public Discount discount { get; set; }


        public Product(string name, string category, int price)
        {
            Name = name;
            Category = category;
            Price = price;


        }

        public Product(ProductPayload payload)
        {
            Name = payload.Name;
            Category = payload.Category;
            Price = payload.Price;


        }

    }

}