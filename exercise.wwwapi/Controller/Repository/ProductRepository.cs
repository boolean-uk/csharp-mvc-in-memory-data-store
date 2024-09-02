using exercise.wwwapi.Model.Models;
using exercise.wwwapi.Model.Data;


namespace exercise.wwwapi.Controller.Repository
{
    public class ProductRepository : IProductRepository
    {
        public List<Product> GetAllProducts()
        {
            return ProductCollection.GetProducts();
        }

        public Product AddProduct(string name, string cathegory, int price)
        {
            return ProductCollection.AddProduct(new Product(name,cathegory, price));
        }

        public Product DeleteProduct(int id)
        {
            return ProductCollection.RemoveProduct(ProductCollection.GetProducts().FirstOrDefault(x => x.Id == id));
        }

        public Product GetAProduct(int id)
        {
            return ProductCollection.GetProducts().FirstOrDefault(x => x.Id == id);
        }

        public Product UppdateProduct(int id, string newname, string newcathegory, int? newprice)
        {
            Product P = ProductCollection.GetProducts().FirstOrDefault(x => x.Id == id);
            if (P is null)
            {
                return null;
            }
            if (newname is not null)
            {
                P.Name= newname;
            }
            if (newcathegory is not null)
            {
                P.Category = newcathegory;
            }
            if (newprice is not null)
            {
                P.Price = (int)newprice;
            }

            return P;
     
            
        }
    }
}
