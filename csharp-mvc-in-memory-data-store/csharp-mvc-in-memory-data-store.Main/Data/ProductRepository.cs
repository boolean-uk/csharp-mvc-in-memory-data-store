﻿using mvc_in_memory_data_store.Data;
using System;

namespace mvc_in_memory_data_store.Models
{
    public class ProductRepository : IProductRepository
    {
        private static int idCounter = 1;
        private static List<Product> _products = new List<Product>();

        public void Create(int id, string name, string category, int price)
        {
            Product product = new Product(id, name, category, price);
            _products.Add(product);
        }

        public List<Product> FindAll()
        {
            return ProductRepository._products;
            
        }

        public Product FindById(int id)
        {
            return _products.First(product => product.Id == id);
        }

        public bool Add(Product product)
        {
            if (product == null)
                return false;
            _products.Add(product);
            return true;
        }

    }
}
