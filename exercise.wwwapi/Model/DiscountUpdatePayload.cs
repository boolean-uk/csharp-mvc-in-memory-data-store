using System.ComponentModel.DataAnnotations;

namespace exercise.wwwapi.Model
{
    public record DiscountUpdatePayload([Required]string? Code, [Required] double Percentage ,int? ProductId);
}
