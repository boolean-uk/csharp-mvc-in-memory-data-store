namespace exercise.wwwapi.Endpoints
{
    public static class DiscountEndpoints
    {

        public static void ConfigureDiscountEndpoints(this WebApplication app)
        {
            // endpoints
            var discountGroup = app.MapGroup("/discount");
        }
    }
}
