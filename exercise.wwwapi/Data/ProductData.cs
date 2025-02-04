namespace exercise.wwwapi.Models
{
    public static class ProductData
    {
        public static List<Product> Products { get; set; } = new List<Product>();

        public static void Initialize()
        {
            Products.Add(new Product { Id = 1, name = "toysoldier", category = "Toy", price = 6});
            Products.Add(new Product { Id = 2, name = "apple", category = "Fruit", price = 1});
            Products.Add(new Product { Id = 3, name = "carrot", category = "Vegetable", price = 2 });
            Products.Add(new Product { Id = 4, name = "Soap", category = "Hygiene", price = 4});
        }
    }
}
