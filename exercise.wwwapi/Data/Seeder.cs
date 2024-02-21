using exercise.wwwapi.Model;

namespace exercise.wwwapi.Data
{
    public class Seeder
    {
        private List<string> _productNames = new List<string>
        {
            "Product 1",
            "Product 2",
            "Product 3",
            "Product 4",
            "Product 5",
            "Product 6",
            "Product 7",
            "Product 8",
            "Product 9",
            "Product 10"
        };
        private List<string> _categories = new List<string> { "Category 1", "Category 2", "Category 3", "Category 4", "Category 5" };
        private List<int> _prices = new List<int> { 100, 200, 300, 400, 500 };
        private List<string> _codes = new List<string> { "Code 1", "Code 2", "Code 3", "Code 4", "Code 5" };
        private List<double> _percentages = new List<double> { 10, 15, 20, 25, 50 };

        private List<Product> _products = new List<Product>();
        private List<Discount> _discounts = new List<Discount>();

        public List<Product> Products { get => _products; }
        public List<Discount> Discounts { get => _discounts; }

        public Seeder()
        {
            Random productRandom = new Random();
            Random discountRandom = new Random();

            for (int i = 1; i < 10; i++)
            {
                Product product = new Product
                {
                    Id = i,
                    Name = _productNames[i - 1],
                    Category = _categories[productRandom.Next(_categories.Count)],
                    Price = _prices[productRandom.Next(_prices.Count)],
        
                };
              _products.Add(product);
            }

            for (int j = 1; j < 5; j++)
            {
                Discount discount = new Discount
                {
                    Id = j,
                    Code = _codes[j - 1],
                    Percentage = _percentages[discountRandom.Next(_percentages.Count)],
                };
                _discounts.Add(discount);
            }
        }
    }
}
