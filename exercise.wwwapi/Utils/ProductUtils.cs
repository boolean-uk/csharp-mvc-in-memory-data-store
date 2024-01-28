using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;

namespace exercise.wwwapi.Utils
{
    public static class ProductUtils
    {
        public static bool ProductNameIsAvailable(IRepository<Product> repository, string? name)
        {
            IEnumerable<Product> entries = repository.Get();
            return !entries.Any(p => p.Name == name);
        }
    }
}
