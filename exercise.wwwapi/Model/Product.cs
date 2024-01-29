using Microsoft.AspNetCore.Antiforgery;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exercise.wwwapi.Model
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public int Price { get; set; }

    }
}
