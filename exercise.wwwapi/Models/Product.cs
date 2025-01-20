using System.ComponentModel.DataAnnotations;

namespace genericapi.api.Models
{
    public class Product : IModel<Guid>
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Category { get; set; }
        public int Price { get; set; }
    }
}
