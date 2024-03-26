using exercise.wwwapi.Controllers.Models;
using System.Xml.Linq;

namespace exercise.wwwapi.Controllers.Data
{
    public class ProductCollection
    {
        public List<Product> collection = new List<Product>()
        {
            new Product(1, "How to build APIs", "Book", 1500)
        };
    }
}
