using Microsoft.AspNetCore.Antiforgery;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace exercise.wwwapi.Model
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public int Price { get; set; }

        public List<Discount> Discounts { get; set; }

    }
}
