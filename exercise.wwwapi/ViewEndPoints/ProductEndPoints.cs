using wwwapi.Data;
using wwwapi.Repository;
using wwwapi.Models;
using wwwapi.Repository;



namespace wwwapi.EndPoints
{
    public static  class ProductEndPoints
    {
        public static async void ConfigureProductEndPoints(this WebApplication app)
        {
            var productGroup = app.MapGroup("products");
            productGroup.MapPost("/", AddProduct);
            productGroup.MapGet("/", GetAllProducts);
            productGroup.MapGet("/{id}", GetProductById);
            productGroup.MapPut("/{id}", UpdateProduct);
            productGroup.MapDelete("/{id}", DeleteProductById);
        }

        public static async Task<IResult> AddProduct(ProductPayload product, IRepository<Product, ProductUpdatePayload> productRepository)
        {
            Product newProd = new Product(product);
            await productRepository.Add(newProd);
            return TypedResults.Ok(newProd);
        }

        public static async Task<IResult> GetAllProducts(IRepository<Product, ProductUpdatePayload> productRepository)
        {
            return TypedResults.Ok(productRepository.GetAll());
        }

        public static async Task<IResult> GetProductById(int id, IRepository<Product, ProductUpdatePayload> productRepository)
        {
            Product? product = await productRepository.Get(id);
            if(product == null) { return TypedResults.NotFound(); }
            return TypedResults.Ok(product);
        }

        public static async Task<IResult> UpdateProduct(int id, IRepository<Product, ProductUpdatePayload> productRepository, ProductUpdatePayload productUpdatePayload)
        {
            Product product = await productRepository.Update(id, productUpdatePayload);
            return TypedResults.Ok(product);

        }

        public static async Task<IResult> DeleteProductById(int id,  IRepository<Product, ProductUpdatePayload> productRepository)
        {
            Product product = await productRepository.Get(id);
            bool res = await productRepository.Delete(id);
            if(res) {return  TypedResults.Ok(product); }
            return TypedResults.NotFound();
        }

    }
}
