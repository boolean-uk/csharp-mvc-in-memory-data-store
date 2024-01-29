using System.ComponentModel;

namespace exercise.wwwapi.Models.Products
{
    public record ProductPutPayload(string? name, string? category, int? price);


}
