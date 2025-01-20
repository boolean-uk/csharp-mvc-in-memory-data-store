namespace exercise.wwwapi.View
{
    public class ProductResponse
    {
        public DateTime When { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Deleted";
        public string Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int Price { get; set; }
}
}
