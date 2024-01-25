using System.ComponentModel.DataAnnotations;

namespace exercise.wwwapi.Model
{
    public class ProductPostPayload
    {

        public string Name { get; set; }

        public string Category { get; set; }

        public int Price { get; set; }

        public ProductPostPayload(string name, string category, int price) 
        {
            Name = name;
            Category = category;
            Price = price;
        }

    }
}
