namespace exercise.wwwapi.Models
{
    public class Product
    {
        public string name { get; set; }
        public string category { get; set; }
        public int price { get; set; }
        public int id { get; set; }

        public Product(string name, string category, int price)
        {
            this.name = name;
            this.category = category;
            this.price = price;
        }

        public Product(string name, string category, int price, int id)
        {
            this.name = name;
            this.category = category;
            this.price = price;
            this.id = id;
        }
    }
}
