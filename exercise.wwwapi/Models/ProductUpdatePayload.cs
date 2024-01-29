using System.ComponentModel;

namespace exercise.wwwapi.Models
{
    public record ProductUpdatePayload(string? name, string? category, int? price);
}
