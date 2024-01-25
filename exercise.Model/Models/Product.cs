namespace exercise.Model.Models
{
    public class Product : DatabaseItem
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public int Price { get; set; }
    }
}
