using System.ComponentModel.DataAnnotations;

namespace exercise.wwwapi.Model
{
    public class Product
    {
        [Key] //Explicitly stating that this is the PrimaryKey...
        public int id { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public int price { get; set; }
    }
}
