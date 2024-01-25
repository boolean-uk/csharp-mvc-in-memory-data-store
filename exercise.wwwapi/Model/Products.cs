

namespace wwwapi.Models
{
    public class Product
    {
        private static int nProducts = 0;
        public string Name { get; set; }
        public string Category{ get; set; }
        public int Price { get; set; }

        public int id { get; init; }


        public Product(string name, string category, int price)
        {
            Name = name;
            Category = category;
            Price = price;
            nProducts++;
            id = nProducts;

        }

        public Product(ProductPayload payload)
        {
            Name = payload.Name;
            Category = payload.Category;
            Price = payload.Price;
            nProducts++;
            id = nProducts;

        }

    }

}