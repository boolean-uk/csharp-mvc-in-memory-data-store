using exercise.wwwapi.Products;
using exercise.wwwapi.Data;


namespace exercise.wwwapi.Repository
{
    public class Repository : IRepository
    {

        private IProduct _productDatabase;
        public Repository(IProduct productDatabase)
        {
            _productDatabase = productDatabase;
        }


        public Product Add(Product product)
        {
            return _productDatabase.Add(product);
        }

        public bool Delete(int id)
        {
            return _productDatabase.Delete(id);
        }

        public IEnumerable<Products.Product> GetProducts()
        {
            return _productDatabase.GetProducts();
        }

        public Product Update(int id, ProductPut productPut)
        {

            var found = _productDatabase.Get(id, out Product product);
            if (!found)
            {
                return null;
            }

            product.Category = productPut.Category;
            product.Price = productPut.Price;
            return product;
        }
    }
    
}
