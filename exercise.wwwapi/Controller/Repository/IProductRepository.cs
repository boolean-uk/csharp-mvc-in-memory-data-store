using exercise.wwwapi.Model.Models;

namespace exercise.wwwapi.Controller.Repository
{
    public interface IProductRepository
    {
        Product AddProduct(string name, string cathegory, int price);
        Product DeleteProduct(int id);
        List<Product> GetAllProducts();
        Product GetAProduct(int id);
        Product UppdateProduct(int id, string newname, string newcathegory, int? newprice);
    }
}
