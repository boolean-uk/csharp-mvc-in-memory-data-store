﻿using Microsoft.AspNetCore.Antiforgery;
using System.ComponentModel.DataAnnotations;

namespace exercise.wwwapi.Model
{
    public class Product
    {
        public int Id { get; set; }

        [MaxLength(12)]
        public string Name { get; set; }

        public string Category { get; set; }

        public int Price { get; set; }
    }
}