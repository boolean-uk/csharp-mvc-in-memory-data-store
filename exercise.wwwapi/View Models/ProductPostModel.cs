﻿using System.ComponentModel.DataAnnotations;

namespace exercise.wwwapi.View_Models
{
    public class ProductPostModel
    {

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }
    }
}
