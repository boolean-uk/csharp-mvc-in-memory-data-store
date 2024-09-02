namespace exercise.wwwapi.Model.Models
{
    public class Product
    {
        private readonly int _id;
        private string _name;
        private string _category;
        private int _price;

        public Product(string name, string category, int price)
        {
            _id = new Random().Next(100000, 999999);
            _name = name;
            _category = category;
            _price = price;
        }

        public int Id { get { return _id; } }
        public string Name { get { return _name; } set { _name = value; } }
        public string Category { get { return _category; } set { _category = value; } }
        public int Price { get { return _price; } set { _price = value; } }

    }
}
