using static exercise.wwwapi.Model.@enum;

namespace exercise.wwwapi.Model
{
    public interface IProduct
    {
        string name { get; set; }
        int price { get; set; }
    }
}
