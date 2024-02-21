namespace exercise.wwwapi.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Category { get; set; }
        public double Price { get; set; }

        public int ? DiscountId { get; set; }
        public Discount Discount { get; set; }
    }
}