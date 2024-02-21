using System.ComponentModel.DataAnnotations;

namespace exercise.wwwapi.Model
{
    public record DiscountCreatePayload([Required] string Code, [Required] double Percentage, int? ProductId);
}
