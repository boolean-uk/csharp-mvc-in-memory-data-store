namespace exercise.wwwapi.Models
{
    public class Product
    {
        public int id { get; set; } = 0;
        public string name { get; set; }
        public string category { get; set; }
        public int price { get; set; }

        public Product Update(UserProduct userProduct)
        {
            this.name = userProduct.name; 
            this.category = userProduct.category; 
            this.price = userProduct.price;
            return this;
        }
    }
}
