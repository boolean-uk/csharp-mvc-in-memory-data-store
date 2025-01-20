namespace exercise.wwwapi.View
{
    public class PostProduct
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Category { get; set; }
        public int Price { get; set; }
    }
}
