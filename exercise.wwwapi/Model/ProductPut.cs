using static exercise.wwwapi.Model.@enum;

namespace exercise.wwwapi.Model
{
    public class ProductPut : IProduct
    {
        public string name { get; set; }
        public int price { get; set; }
    }
}
