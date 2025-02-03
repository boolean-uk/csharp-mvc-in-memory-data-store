using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public class Repository : IRepository
    {
        public IEnumerable<Product> GetProducts()
        {
            return ProductData.Products;
        }

        public Product GetProduct(int id)
        {
            var product = ProductData.Products.FirstOrDefault(prod => prod.Id == id);
            return product;
        }

        public Product AddProduct(ProductPost model)
        {
            int nextId = ProductData.Products.Max(prod => prod.Id) + 1;
            Product product = new Product()
            {
                Id = nextId,
                name = model.name,
                category = model.category,
                price = (int)model.price
            };
            ProductData.Products.Add(product);
            return product;
        }

        public Product UpdateProduct(int id, ProductPut model)
        {
            var target = ProductData.Products.FirstOrDefault(prod => prod.Id == id);
            if (target == null) return target;                                          //  Handle Not Found
            if (model.name != null) target.name = model.name;
            if (model.category != null) target.category = model.category;
            if (model.price != null) target.price = (int)model.price;
            return target;
        }

        public bool DeleteProduct(int id)
        {
            return ProductData.Products.RemoveAll(prod => prod.Id == id) > 0 ? true : false;
        }
    }
}
