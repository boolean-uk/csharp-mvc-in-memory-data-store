using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exercise.Model.DTOs
{
    public class AddProductDTO
    {
        public string Name { get; set; } = "Product";
        public string Category { get; set; } = "Category";
        public int Price { get; set; }
    }
}
