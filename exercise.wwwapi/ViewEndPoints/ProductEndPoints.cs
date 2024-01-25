using wwwapi.Data;
using wwwapi.Repository;
using wwwapi.Models;
using wwwapi.Repository;



namespace wwwapi.EndPoints
{
    public static class ProductEndPoints
    {
        public static void ConfigureProductEndPoints(this WebApplication app)
        {
            var productGroup = app.MapGroup("products");
            productGroup.MapPost("/", AddProduct);
            productGroup.MapGet("/", GetAllProducts);
            productGroup.MapGet("/{id}", GetProductById);
            productGroup.MapPut("/{id}", UpdateProduct);
            productGroup.MapDelete("/{id}", DeleteProductById);
        }

        public static IResult AddProduct(ProductPayload product, IRepository<Product> productRepository)
        {
            Product newProd = new Product(product);
            productRepository.Add(newProd);
            return TypedResults.Ok(newProd);
        }

        public static IResult GetAllProducts(IRepository<Product> productRepository)
        {
            return TypedResults.Ok(productRepository.GetAll());
        }

        public static IResult GetProductById(int id, IRepository<Product> productRepository)
        {
            Product? product = productRepository.Get(id);
            if(product == null) { return TypedResults.NotFound(); }
            return TypedResults.Ok(product);
        }

        public static IResult UpdateProduct(string name, Product updatedProduct, IRepository<Product> productRepository)
        {
            throw new NotImplementedException();
        }

        public static IResult DeleteProductById(string name, IRepository<Product> productRepository)
        {
            throw new NotImplementedException();
        }

    }
}
