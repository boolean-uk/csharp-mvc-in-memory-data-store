namespace exercise.wwwapi.Models
{
    public record ProductUpdatePayload(string? Name, string? Category, int? Price);

    public record ProductPostPayload(string Name, string Category, int Price);

    public record DiscountUpdatePayload(int DiscountId, int ProductId);
}