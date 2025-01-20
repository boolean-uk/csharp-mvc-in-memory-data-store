using Microsoft.AspNetCore.Mvc;
using workshop.wwwapi.Models;
using workshop.wwwapi.Repository;
using workshop.wwwapi.ViewModel;

namespace workshop.wwwapi.Endpoints
{
    public static class ProductEndpoint
    {
        public static void ConfigureProductEndpoint(this WebApplication app)
        {
            var products = app.MapGroup("products");

            products.MapGet("/", GetProducts);
            products.MapGet("/{id}", GetProduct);
            products.MapPost("/", AddProduct);
            products.MapDelete("/{id}", DeleteProduct);
            products.MapPut("/{id}", UpdateProduct);
        }

        #region Get request
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetProducts(IProductRepository repository, string? category)
        {
            if (string.IsNullOrEmpty(category))
            {
                var products = await repository.GetProducts(null);
                return TypedResults.Ok(products);
            }
            else
            {
                var products = await repository.GetProducts(category.ToLower());

                if (products != null) 
                {
                    return TypedResults.Ok(products); 
                } 
                else
                {
                    return TypedResults.NotFound($"No products of the provided '{category}' were found");
                }
                
            }
            
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetProduct(IProductRepository repository, int id)
        {
            var product = await repository.GetProduct(id);
            if (product == null) return TypedResults.NotFound("Product not found");
            return TypedResults.Ok(product);
        }
        #endregion

        #region Post request
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> AddProduct(IProductRepository repository, ProductPost model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.Category) || model.Price <= 0)
                {
                    return TypedResults.BadRequest("Price must be an integer, something else was provided. / Product with provided name already exists");
                }
                else
                {
                    Product product = new Product()
                    {
                        Name = model.Name,
                        Category = model.Category,
                        Price = model.Price
                    };
                    await repository.AddProduct(product);

                    return TypedResults.Created($"https://localhost:7188/products/{product.Id}", product);

                }
            
            }
            catch (Exception ex)
            {
                return TypedResults.Problem(ex.Message);
            }
        }
        #endregion

        #region Delete request
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> DeleteProduct(IProductRepository repository,int id)
        {                        
            try
            {
                var model = await repository.GetProduct(id);
                if (await repository.DeleteProduct(id)) return Results.Ok(new { When=DateTime.Now, Status="Deleted", Name=model.Name, Category = model.Category, Price =model.Price });
                return TypedResults.NotFound("Product not found");
            }
            catch (Exception ex)
            {
                return TypedResults.Problem(ex.Message);
            }
        }
        #endregion

        #region Put request
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> UpdateProduct(IProductRepository repository, int id,  ProductPut model)
        {
            try
            {
                var target = await repository.GetProduct(id);
                if (target == null) return Results.NotFound("Product not found.");
                if (model.Name != null) target.Name = model.Name; else return Results.BadRequest("Price must be an integer, something else was provided. / Product with provided name already exists");
                if (model.Category != null) target.Category = model.Category; else return Results.BadRequest("Price must be an integer, something else was provided. / Product with provided name already exists");
                if (model.Price != null) target.Price = model.Price.Value; else return Results.BadRequest("Price must be an integer, something else was provided. / Product with provided name already exists");
                repository.UpdateProduct(target);
                return TypedResults.Ok(target);
            }
            catch (Exception ex)
            {
                return TypedResults.Problem(ex.Message);
            }
        }
        #endregion

    }
}
