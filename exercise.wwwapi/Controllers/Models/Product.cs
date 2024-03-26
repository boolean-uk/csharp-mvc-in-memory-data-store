namespace exercise.wwwapi.Controllers.Models
{
    public class Product
    {
        public int id { get; set; }
        public string name { get; set; }   
        public string category { get; set; }
        public int price { get; set; }

        public Product(int id, string name, string category, int price)
        {
            this.id = id;
            this.name = name;
            this.category = category;
            this.price = price;
        }

        public int getId()
        {
            return this.id;
        }

        public String getName()
        {
            return this.name;
        }

        public String getCategory()
        {
            return this.category;
        }

        public int getPrice()
        {
            return this.price;
        }
    }
}
