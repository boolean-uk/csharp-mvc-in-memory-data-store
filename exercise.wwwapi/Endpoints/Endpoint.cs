using exercise.wwwapi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using exercise.wwwapi.Models;
using Microsoft.VisualBasic;
using NuGet.Protocol.Plugins;

namespace exercise.wwwapi.Endpoints
{
    public static class Endpoint
    {
        public static void ConfigureEndpoint(this WebApplication app)
        {
            var ProductGroup = app.MapGroup("/products");

            ProductGroup.MapGet("/", GetProducts);
            
            ProductGroup.MapGet("/{id}", GetAProduct);
            ProductGroup.MapPost("/", AddProduct);
            ProductGroup.MapPut("/{id}", UpdateProduct);
            ProductGroup.MapDelete("/{id}", DeleteProduct);

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public static async Task<IResult> GetProducts(IRepository repository, string category)
        {
            var Result = repository.GetByCategory(category);
            if (Result == null)
            {
                return TypedResults.NotFound(repository.GetAll());
            }
            return TypedResults.Ok(Result);
            
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetAProduct(IRepository repository, int productId)
        {
            var Result = repository.GetById(productId);
            if(Result == null)
            {
                return TypedResults.NotFound("Product not found.");
            }
            return TypedResults.Ok(Result);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> AddProduct(IRepository repository, ProductPost model)
        {
                        
                if (model != null)
                {
                if (Math.Abs(model.Price) >= (int)0)
                {
                    IEnumerable<Product> existingProducts = repository.GetAll();

                    if (existingProducts.Count() > 0)
                    {
                        Product product1 = existingProducts.FirstOrDefault(x => x.Name.ToLower().Equals(model.Name.ToLower()));
                        if (product1 != null)
                        {
                            return TypedResults.BadRequest("Product with provided name already exists.");
                        }
                    }
                    
                    Product product = repository.CreateProduct(model);
                    return TypedResults.Created($"/{product.Id}", product);
                }else { return TypedResults.BadRequest("Price must be an integer, something else was provided."); }

                }
                else { return TypedResults.BadRequest("not found"); }
          
        }


        [ProducesResponseType(StatusCodes.Status201Created)]

        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public static async Task<IResult> UpdateProduct(IRepository repository, int id, ProductPost model)
        {
            Product product;

            IEnumerable<Product> existingProducts = repository.GetAll();

            if ((model != null) && (existingProducts.Count()>0)) {

                if (Math.Abs(model.Price) >= (int)0)
                {
                        Product productName = existingProducts.FirstOrDefault(x => x.Name.ToLower().Equals(model.Name.ToLower()));
                        if (productName != null)
                        {
                            return TypedResults.BadRequest("Product with provided name already exists.");
                        }
                        else
                        { product = repository.UpdateProduct(id, model);

                        if (product == null)
                            {
                                return TypedResults.NotFound("Not Found.");
                            }
                            else
                            {
                                return TypedResults.Created($"/{product.Id}", product);
                            }
                        }
                                        
                }
                else { return TypedResults.BadRequest("Price must be an integer, something else was provided."); }

            }else { return TypedResults.BadRequest("Bad request"); }

           

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public static async Task<IResult> DeleteProduct(IRepository repository, int id)
        {
            Product product = repository.DeleteProduct(id);
            if(product!= null) 
            { 
                return TypedResults.Created($"/{product.Id}", product); 
            }
            else { return TypedResults.NotFound("Not Found."); }

           

        }


    }
}
