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

        List<Product> products = await productsRepository.GetAllAsync();
        if (products.Any(p => p.Name.ToLower() == name.ToLower()))
        {
            return Results.BadRequest($"Already a product with name '{name}'!");
        }

        Product product = new Product(name, category, parsedPrice);

        await productsRepository.AddAsync(product);

        return Results.Created($"https://localhost:7010/products/{product.UUID}", product);
    }

    public static async Task<IResult> GetAll(ProductsRepository productsRepository, string? category)
    {
        List<Product> products;
        if (!string.IsNullOrEmpty(category))
        {
            products = await productsRepository.GetAllAsync();
            products = products.Where(p => p.Category.ToLower() == category.ToLower()).ToList();
        }
        else
        {
            products = await productsRepository.GetAllAsync();
        }

        if (products.Count > 0)
        {
            return Results.Ok(products);
        }
        else
        {
            return Results.NotFound($"No products found in category '{category}' or no products found.");
        }
    }

    public static async Task<IResult> Get(ProductsRepository productsRepository, Guid UUID)
    {
        Product product = await productsRepository.GetAsync(UUID);
        if (product != null)
        {
            return Results.Ok(product);
        }
        else
        {
            return Results.NotFound($"No products found with UUID '{UUID}'");
        }
    }

    public static async Task<IResult> Update(ProductsRepository productsRepository, Guid UUID, string? name, string? category, string? price)
    {
        Product product = await productsRepository.GetAsync(UUID);
        if (product == null)
        {
            return Results.NotFound($"No products found with UUID '{UUID}'");
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
            List<Product> products = await productsRepository.GetAllAsync();
            if (products.Any(p => p.Name.ToLower() == name.ToLower()))
            {
                return Results.BadRequest($"Already a product with name '{name}'!");
            }
        }

        Product updatedProduct = await productsRepository.UpdateAsync(UUID, name, category, price != null ? Convert.ToInt32(price) : product.Price);

        if (updatedProduct != null)
        {
            return Results.Ok(updatedProduct);
        }

        return Results.BadRequest();
    }

    public static async Task<IResult> Delete(ProductsRepository productsRepository, Guid UUID)
    {
        Product product = await productsRepository.GetAsync(UUID);
        if (product != null)
        {
            await productsRepository.DeleteAsync(UUID);
            return Results.Ok(new { Status = "Deleted", Product = product });
        }
        return Results.NotFound($"No products found with UUID '{UUID}'");
    }
}
