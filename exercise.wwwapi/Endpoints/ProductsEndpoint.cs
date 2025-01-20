using Microsoft.AspNetCore.Mvc;

public static class ProductsEndpoint
{
    public static void ConfigureProductsEndpoint(this WebApplication app)
    {
        var products = app.MapGroup("products");

        products.MapPost("/", Add);
        products.MapGet("/", GetAll);
        products.MapGet("/{id}", Get);
        products.MapDelete("/{id}", Delete);
        products.MapPut("/{id}", Update);
    }

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public static async Task<IResult> Add(ProductsRepository productsRepository, string name, string category, string? price)
    {
        if (string.IsNullOrEmpty(price) || !int.TryParse(price, out int parsedPrice))
        {
            return Results.BadRequest($"Price must be a valid integer, not '{price}'!");
        }

        if (parsedPrice < 0)
        {
            return Results.BadRequest($"Price must be a positive value, not '{parsedPrice}'!");
        }

        List<Product> products = productsRepository.GetAll().Where(p => p.Name.ToLower() == name.ToLower()).ToList();
        if (products.Count > 0)
        {
            return Results.BadRequest($"Already a product with name '{name}'!");
        }

        Product product = new Product(name, category, parsedPrice);

        productsRepository.Add(product);

        return Results.Created($"https://localhost:7010/products/{product.UUID}", product);
    }


    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public static async Task<IResult> GetAll(ProductsRepository productsRepository, string? category)
    {
        List<Product> products;
        if (!string.IsNullOrEmpty(category))
        {
            products = productsRepository.GetAll().Where(p => p.Category.ToLower() == category.ToLower()).ToList();
        }
        else
        {
            products = productsRepository.GetAll();
        }

        if (products.Count > 0)
        {
            return Results.Ok(products);
        }
        else
        {
            return Results.NotFound($"No products found in category '{category}'.");
        }
    }


    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public static async Task<IResult> Get(ProductsRepository productsRepository, Guid UUID)
    {
        Product product = productsRepository.Get(UUID);
        if (product != null)
        {
            return Results.Ok(product);
        }
        else
        {
            return Results.NotFound($"No products found with UUID '{UUID}'.");
        }
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public static async Task<IResult> Update(ProductsRepository productsRepository, Guid UUID, string? name, string? category, string? price)
    {
        Product product = productsRepository.Get(UUID);
        if (product == null)
        {
            return Results.NotFound($"No products found with UUID '{UUID}'.");
        }

        if (!string.IsNullOrEmpty(price))
        {
            if (!int.TryParse(price, out int parsedPrice))
            {
                return Results.BadRequest($"Price must be a valid integer, not '{price}'!");
            }

            if (parsedPrice < 0)
            {
                return Results.BadRequest($"Price must be a positive value, not '{parsedPrice}'!");
            }

            price = parsedPrice.ToString();
        }
        else
        {
            price = product.Price.ToString();
        }

        if (!string.IsNullOrWhiteSpace(name))
        {
            List<Product> products = productsRepository.GetAll().Where(p => p.Name.ToLower() == name.ToLower()).ToList();
            if (products.Count > 0)
            {
                return Results.BadRequest($"Already a product with name '{name}'!");
            }
        }

        Product updatedProduct = productsRepository.Update(UUID, name, category, price != null ? Convert.ToInt32(price) : product.Price);

        if (updatedProduct != null)
        {
            return Results.Ok(updatedProduct);
        }

        return Results.BadRequest();
    }


    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public static async Task<IResult> Delete(ProductsRepository productsRepository, Guid UUID)
    {
        Product product = productsRepository.Get(UUID);
        if (product != null)
        {
            productsRepository.Delete(UUID);
            return Results.Ok(new { Status = "Deleted", Product = product });
        }
        return Results.NotFound($"No products found with UUID '{UUID}'.");
    }

}
