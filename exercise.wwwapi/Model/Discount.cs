using System.Text.Json.Serialization;
using wwwapi.Models;

namespace exercise.wwwapi.Model
{
    public class Discount
    {
        public int Id { get; set; }
        public int percentage { get; set; }
        public float DiscountPercentage {  get { return ((float)percentage / 100f); } }

        [JsonIgnore]
        public List<Product> Products { get; set; }
    }
}
