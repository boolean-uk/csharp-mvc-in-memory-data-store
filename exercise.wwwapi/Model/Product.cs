namespace exercise.wwwapi.Model
{
    public class Product
    {
        public int id { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        private int _price;
        public int price { get { return newPrice(); } set { _price = value; } }

        public Discount? discount;

        private int newPrice()
        {
            if (discount is null) return _price;
            return _price - (_price * discount.percentage / 100);
        }
    }
}
