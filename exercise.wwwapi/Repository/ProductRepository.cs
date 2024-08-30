using exercise.wwwapi.Data;
using exercise.wwwapi.Models;
using Microsoft.OpenApi.Validations;
using static System.Reflection.Metadata.BlobBuilder;

namespace exercise.wwwapi.Repository
{
    public class ProductRepository : IRepository
    {
        ProductCollection pc;

        public ProductRepository(ProductCollection pc)
        {
            this.pc = pc;
        }
        public Product CreateProduct(Payload payload)
        {
            

            Product newProduct = new Product(payload.name, payload.category, payload.price, 0);
            this.pc.products.Add(newProduct);
            this.pc.SaveChanges();
            return newProduct;
        }
        public List<Product> GetProducts(string category)
        {
            return this.pc.products.Where(x=>x.category == category).ToList();
        }
        public Product GetProduct(int id)
        {
            var product = this.pc.products.FirstOrDefault(p => p.id == id);
            return product;
        }
        public Product UpdateProduct(Payload payload, int id)
        {
            var product = this.pc.products.FirstOrDefault(x=>x.id == id);
            product.price = payload.price;
            product.category = payload.category;
            product.name = payload.name;
            this.pc.SaveChanges();
            return product;
        }
        public Product DeleteProduct(int id)
        {
            var product = this.pc.products.FirstOrDefault(x => x.id == id);
            this.pc.products.Remove(product);
            this.pc.SaveChanges();
            return product;
        }
        
        public bool ContainsProduct(string name)
        {
            foreach (var item in pc.products)
            {
                if(item.name == name)
                {
                    return true;
                }
            }
            return false;
        }

        public bool ContainsProduct(int id)
        {
            foreach (var item in pc.products)
            {
                if (item.id == id)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
