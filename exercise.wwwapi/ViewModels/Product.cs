namespace exercise.wwwapi.ViewModels
{
    public record ProductPost(string Name, string Category, int Price) { }
    public record ProductPut(string? Name, string? Category, int? Price) { }
}
