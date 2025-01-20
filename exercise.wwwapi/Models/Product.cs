namespace exercise.wwwapi.model;

public record Product
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Category { get; set; }
    public required int Price { get; set; }
}
