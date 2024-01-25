﻿using exercise.wwwapi.Data;
using exercise.wwwapi.Models;
using Microsoft.AspNetCore.Authentication;
using System.Xml.Linq;

namespace exercise.wwwapi.Repository
{
    public class ProductRepository : IProductRepository
    {
        private ProductContext _db;
        private int _id = 0;

        public ProductRepository(ProductContext db)
        {
            this._db = db;
        }

        public List<Product> GetAllProducts()
        {
            return _db.Products.ToList();
        }

        public Product AddProduct(string name, string catagory, int price)
        {
            List<Product> products = _db.Products.ToList();
            for (int i = 0; i < products.Count(); i++)
            {
                if (products[i].Name == name)
                    return null;
            }

            var newProduct = new Product() { ID = _id++, Name = name, Catagory = catagory, Price = price };
            _db.Add(newProduct);
            _db.SaveChanges();
            return newProduct;
        }

        public Product? GetProduct(int id)
        {
            return _db.Products.FirstOrDefault(t => t.ID == id);
        }

        public Product? UpdateProduct(Product products, ProductUpdatePayload updateData)
        {
            bool hasUpdate = false;
            List<Product> productList = _db.Products.ToList();

            if (updateData.name != null)
            {
                for (int i = 0; i < productList.Count(); i++)
                {
                    if (productList[i].Name == updateData.name)
                        return null;
                }

                products.Name = (string)updateData.name;
                hasUpdate = true;
            }

            if (updateData.price != null)
            {
                products.Price = (int)updateData.price;
                hasUpdate = true;
            }

            if (updateData.catagory != null)
            {
                products.Catagory = (string)updateData.catagory;
                hasUpdate = true;
            }

            if (!hasUpdate) throw new Exception("No update data provided");
            _db.SaveChanges();
            return products;
        }

        public bool DeleteProduct(int id)
        {
            var product = GetProduct(id);
            if (product == null) return false;
            _db.Products.Remove(product);
            _db.SaveChanges();
            return true;
        }
    }
}
