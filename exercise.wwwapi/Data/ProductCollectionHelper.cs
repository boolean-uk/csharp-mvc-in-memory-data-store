using System;
using System.Collections.Generic;
using System.Linq;
using exercise.wwwapi.Models;

namespace exercise.wwwapi.Data
{
    public class ProductCollectionHelper : IColl<Product>
    {
        private List<Product> _products = new List<Product>()
        {
            new Product() { Id = 1, Name = "Laptop", Category = "Electronics", Price = 1200 },
            new Product() { Id = 2, Name = "Espresso Machine", Category = "Kitchen Appliances", Price = 800 },
            new Product() { Id = 3, Name = "Acoustic Guitar", Category = "Electronics", Price = 700 },
            new Product() { Id = 4, Name = "Smartphone", Category = "Electronics", Price = 999 },
            new Product() { Id = 5, Name = "Ergonomic Chair", Category = "Office Supplies", Price = 350 },
            new Product() { Id = 6, Name = "LED Desk Lamp", Category = "Office Supplies", Price = 45 },
            new Product() { Id = 7, Name = "Bluetooth Headphones", Category = "Electronics", Price = 250 },
            new Product() { Id = 8, Name = "Running Shoes", Category = "Fitness", Price = 150 },
            new Product() { Id = 9, Name = "Yoga Mat", Category = "Fitness", Price = 35 },
            new Product() { Id = 10, Name = "Smartwatch", Category = "Electronics", Price = 199 },
            new Product() { Id = 11, Name = "IT by Stephen King", Category = "Books", Price = 20 },
            new Product() { Id = 12, Name = "Dune by Frank Herbert", Category = "Books", Price = 25 },
            new Product() { Id = 13, Name = "Water Bottle", Category = "Fitness", Price = 15 },
            new Product() { Id = 14, Name = "Backpack", Category = "Office Supplies", Price = 120 },
            new Product() { Id = 15, Name = "Wireless Mouse", Category = "Electronics", Price = 60 },
            new Product() { Id = 16, Name = "Desk Organizer", Category = "Office Supplies", Price = 30 },
            new Product() { Id = 17, Name = "Action Camera", Category = "Electronics", Price = 350 },
            new Product() { Id = 18, Name = "Noise Cancelling Headphones", Category = "Electronics", Price = 300 },
            new Product() { Id = 19, Name = "E-Reader", Category = "Electronics", Price = 130 },
            new Product() { Id = 20, Name = "Bluetooth Speaker", Category = "Electronics", Price = 110 }
        };

        public Product Insert(Product product)
        {
            product.Id = _products.Max(b => b.Id) + 1;
            _products.Add(product);
            return product;
        }

        public IEnumerable<Product> GetAll()
        {
            return _products.ToList();
        }

        public Product GetById(object id)
        {
            return _products.FirstOrDefault(product => product.Id == (int)id);
        }

        public Product Remove(object id)
        {
            var product = GetById(id);
            if (product != null)
            {
                _products.Remove(product);
            }
            return product;
        }

        public Product Update(Product entity)
        {
            var product = GetById(entity.Id);
            if (product != null)
            {
                product.Name = entity.Name;
                product.Price = entity.Price;
                product.Category = entity.Category;
            }
            return product;
        }
    }
}
