namespace mvc_in_memory_data_store.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }

        //public Product(int Id, string Name, string Category, int Price)
        //{
        //    this.Id = Id;
        //    this.Name = Name;
        //    this.Category = Category;
        //    this.Price = Price;
        //}

    }
}
