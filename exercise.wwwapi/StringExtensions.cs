namespace exercise.wwwapi
{
    public static class StringExtensions
    {
        public static bool IsNumeric(this string text) => double.TryParse(text, out _);
    }
}
