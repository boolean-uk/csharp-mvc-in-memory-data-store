using System.ComponentModel.DataAnnotations;

namespace exercise.wwwapi.Model
{
    public record ProductCreatePayload([Required] string Name, [Required] string Category, [Required] double Price);
}
