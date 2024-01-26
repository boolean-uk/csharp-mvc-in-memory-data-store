using static exercise.wwwapi.Model.@enum;

namespace exercise.wwwapi.Model
{
    public class Product : IProduct
    {
        public int id { get; set; }
        public string name { get; set; }
        public ProductType type { get; set; }
        public int price { get; set; }
    }
}
