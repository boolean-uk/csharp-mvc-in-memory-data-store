namespace mvc_in_memory_data_store.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name {  get; set; }
        public string Category { get; set; }
        public int Price { get; set; }
        public Product(int id, String name, String category, int price)
        {
            this.Id = id;
            this.Name = name;
            this.Category = category;
            this.Price = price;
        }

        public int getId()
        {
            return this.Id;
        }

        public String getName()
        {
            return this.Name;
        }

        public String getCategory()
        {
            return this.Category;
        }

        public int getPrice()
        {
            return this.Price;
        }
    }
}
