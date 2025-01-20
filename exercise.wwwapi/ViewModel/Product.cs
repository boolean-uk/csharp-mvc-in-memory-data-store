namespace exercise.wwwapi.viewmodel;

public record ProductPut
{
    public string? Name { get; set; }
    public string? Category { get; set; }
    public int? Price { get; set; }
}

public record ProductPost
{
    public required string Name { get; set; }
    public required string Category { get; set; }
    public required int Price { get; set; }
}
