using System.ComponentModel.DataAnnotations;

namespace exercise.wwwapi.View
{
    public class Product_create
    {
        //private Product_create() { }
        public bool valid ()
        {
            return name != null && category != null && price != null;
        }
        public string name { get; set; } 
        public string category { get; set; }
        public int price { get; set; }
    }
}
