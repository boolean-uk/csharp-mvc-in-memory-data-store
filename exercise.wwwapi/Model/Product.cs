namespace exercise.wwwapi.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int Price { get; set; }
    }

    public class InPuProduct
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Price { get; set; }
    }
}
