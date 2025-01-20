namespace exercise.wwwapi.Model
{
    public class Product
    {
        public required string Id { get; set; } = Guid.NewGuid().ToString();
        public required string Name { get; set; }
        public required string Category { get; set; }
        public required int Price { get; set; }
    }
}
