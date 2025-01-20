namespace exercise.wwwapi.ViewModel
{
    public class ProductResponse
    {
        public DateTime When { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Deleted";
        public string Name { get; set; }
        public string Category { get; set; }
        public int Price { get; set; }
    }
}
