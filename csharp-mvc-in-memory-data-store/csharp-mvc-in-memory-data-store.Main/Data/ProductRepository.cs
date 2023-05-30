using mvc_in_memory_data_store.Data;
using System;

namespace mvc_in_memory_data_store.Models
{
    public class ProductRepository : IProductRepository
    {
        private static int IdCounter = 1;
        private static List<Product> _products = new List<Product>();

        //public void create(string name, string category, int price)
        //{
        //    Product product = new Product();
        //    product.id = ProductRepository.IdCounter++;
        //    product.Name = name;
        //    product.Category = category;
        //    product.Price = price;
        //    ProductRepository._products.Add(product);
        //}

        public List<Product> GetAll()
        {
            return ProductRepository._products; 
        }

        public Product GetById(int id)
        {
            //var result = _products.Where(x => x.id == id).FirstOrDefault();
            //return result;
            return ProductRepository._products.FirstOrDefault(p => p.id == id);
        }

        public bool AddProduct(Product product)
        {
            if (product != null)
            {
                product.id = ProductRepository.IdCounter++;
                _products.Add(product);
                return true;
            }
            return false;
        }

        public bool ChangeById(int id, Product product)
        {
            if (_products.Any(x => x.id == id))
            {
                var changeProduct = _products.FirstOrDefault(i => i.id == id);
                if (changeProduct != null)
                {
                    product.id = id;
                    changeProduct.Name = product.Name ;
                    changeProduct.Category = product.Category;
                    changeProduct.Price = product.Price;
                }
                return true;
            } return false;
            // zorgen dat je niet alles hoef in te vullen maar alleen wat je wilt veranderen??
            //var result = ProductRepository._products.FirstOrDefault(p => p.id == id);
            //_products.Remove(product);
            //return result;
        }

        public Product RemoveById(int id)
        {
            if (_products.Any(x => x.id == id))
            {
                var p = _products.FirstOrDefault(i => i.id==id);
                if (p != null)
                {
                    _products.Remove(p);
                }
                return p;
            }
            return null;
        }
    }
}
