using exercise.wwwapi.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace exercise.wwwapi.Data
{
    public class ErrorCollection
    {
        public List<ErrorTypes> errors;

        public ErrorCollection()
        {
            errors = new List<ErrorTypes>();
            errors.Add(new ErrorTypes("Price must be an integer, something else was provided. / Product with provided name already exists.", 400));
        }
        public ErrorTypes? GetError(int id)
        {
            return errors.FirstOrDefault(e => e.getId() == id);
        }
    }
}
