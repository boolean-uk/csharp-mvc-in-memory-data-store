namespace exercise.wwwapi.Models
{
    public class ProductsPut : IProducts
    {
        public string name {get;set;}
        public string category { get; set; }
        public int price { get; set; }
    }
}
