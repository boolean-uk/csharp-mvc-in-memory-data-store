namespace mvc_in_memory_data_store.Models
{
    public class Product
    {
        public Product(int id, string name, string category, int price)
        {
            Id = id;
            Name = name;
            Category = category;
            Price = price;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int Price { get; set; }
    }
}
