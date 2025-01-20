namespace exercise.wwwapi.View
{
    public class PutProduct
    {
        // Id cannot be changed
        public string? Name { get; set; }
        public string? Category { get; set; }
        public int? Price { get; set; }
    }
}
