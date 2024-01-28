namespace exercise.wwwapi.Models
{
    public class ProductPost
    {
        public ProductPost(string name, string category, int price)
        {
            Name = name;
            Category = category;
            Price = price;
        }

        public string Name { get; }
        public string Category { get; }
        public int Price { get; }
    }
}
