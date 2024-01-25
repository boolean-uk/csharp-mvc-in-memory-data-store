﻿using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoint
    {
        public static void ProductLogics(this WebApplication app)
        {
            var productGroup = app.MapGroup("products");
            productGroup.MapGet("/", GetAllProducts);
            productGroup.MapGet("/{id}", GetProduct);
            productGroup.MapPost("/", CreateProduct);
            productGroup.MapPut("/{id}", UpdateProduct);
            productGroup.MapDelete("/{id}", DeleteProduct);
        }

        public static IResult GetAllProducts(IProductRepository products)
        {
            return TypedResults.Ok(products.GetAllProducts());
        }

        public static IResult GetProduct(IProductRepository products, int id)
        {
            Product? product = products.GetProduct(id);
            if (product == null)
                return Results.NotFound("ID out of scope");

            return TypedResults.Ok(products.GetProduct(id));
        }

        public static IResult CreateProduct(IProductRepository products, ProductPostPayload createdProduct)
        {
            Product? product = products.AddProduct(createdProduct.name, createdProduct.catagory, createdProduct.price);
            if (product == null)
                return Results.NotFound("Could not create student");

            return TypedResults.Created($"/products{product.Name} {product.Catagory} {product.Price}", product);
        }

        public static IResult UpdateProduct(IProductRepository products, ProductUpdatePayload posted, int id)
        {
            Product? product = products.GetProduct(id);
            if (product == null)
                return Results.NotFound("ID out of scope");

            product = products.UpdateProduct(product, posted);
            return TypedResults.Created($"/products{product.Name} {product.Catagory} {product.Price}", product);
        }

        public static IResult DeleteProduct(IProductRepository products, int id)
        {
            Product? product = products.GetProduct(id);
            if (product == null)
                return Results.NotFound("ID out of scope");

            return TypedResults.Ok(products.DeleteProduct(id));
        }
    }
}
