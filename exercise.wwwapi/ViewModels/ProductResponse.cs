namespace exercise.wwwapi.ViewModels
{
    public class ProductResponse
    {
        public DateTime When { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Deleted";
        public string name { get; set; }
        public string category { get; set; }
        public int price { get; set; }
    }
}
