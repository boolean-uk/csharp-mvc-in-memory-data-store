using exercise.wwwapi.Model;
using exercise.wwwapi.Repository;
using Microsoft.AspNetCore.Mvc;


namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoints
    {
        [ProducesResponseType(StatusCodes.Status200OK)]
        public static void ConfigureProductEndpoints(this WebApplication app)
        {
            var productGroup = app.MapGroup("/products");
            productGroup.MapGet("/", GetAllProducts);
            productGroup.MapPost("/", CreateProduct);
            productGroup.MapGet("/{id}", GetProduct);
            productGroup.MapPut("/{id}", UpdateProduct);
            productGroup.MapDelete("/{id}", DeleteProduct);
        }
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult GetAllProducts(IProductRepository productRepository)
        {
          //Products products = productRepository.GetAllProducts();
          //if(products.Category == null) return TypedResults.NotFound("No products of the provided category were found.");
            return TypedResults.Ok(productRepository.GetAllProducts());
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static IResult CreateProduct(IProductRepository productRepository, ProductPostPayload newProductPostData)
        {
            // checks if price is bigger than 0 and a positive number
            if (newProductPostData.Price <= 0) return TypedResults.BadRequest("Price must be an integer or bigger than 0, something else was provided.");
            // checks if Name is NotNull or Empty
            if (string.IsNullOrEmpty(newProductPostData.Name)) return TypedResults.BadRequest("Product name can not empty.");
            // checks if name exists
            if (productRepository.ProductExists(newProductPostData.Name)) return TypedResults.BadRequest($"Product with name {newProductPostData.Name} already exists");
            Products product = productRepository.AddProduct(newProductPostData.Name, newProductPostData.Category, newProductPostData.Price);
            return TypedResults.Created($"Product with {product.Id} is created", product);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult GetProduct(IProductRepository productRepository, int id)
        {
            Products? product = productRepository.GetProductById(id);
            if (product == null) return TypedResults.NotFound($"Product with id {id} is not found.");
            return TypedResults.Ok(product);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static IResult UpdateProduct(IProductRepository productRepository, int id, ProductUpdatePayload newUpdateData) 
        {
            try
            {
                Products? product = productRepository.UpdateProduct(id, newUpdateData);
                if (newUpdateData.Price <= 0) return TypedResults.BadRequest("Price must be an integer or bigger than 0, something else was provided.");
                if (product == null) return TypedResults.NotFound($"Product with id {id} is not found.");
                return TypedResults.Ok(product);
            }
            catch (Exception ex) 
            {
                return Results.BadRequest(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult DeleteProduct(IProductRepository productRepository, int id)
        {
            if (!productRepository.DeleteProduct(id)) return TypedResults.NotFound($"Product with id {id} is not found!");
            return Results.NoContent();
        }
    }
}
