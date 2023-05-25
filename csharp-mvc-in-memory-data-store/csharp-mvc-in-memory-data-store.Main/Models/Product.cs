﻿namespace mvc_in_memory_data_store.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }

        public Product(int id, string name, string category, decimal price)
        {
            this.Id = id;
            this.Name = name;
            this.Category = category;
            this.Price = price;
        }

        public int getId()
        {
            return this.Id;
        }
    }
}