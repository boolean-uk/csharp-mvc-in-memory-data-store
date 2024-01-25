﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace exercise.wwwapi.Models
{
    public class ProductPost
    {

        public string Name { get; set; }
        public string Category { get; set; }
        public int Price { get; set; }
    }
}
