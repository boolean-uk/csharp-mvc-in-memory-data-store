
public class ProductView
{
    public static void RenderProduct(Product product)
    {
        Console.WriteLine("Product:");
        Console.WriteLine($"UUID: {product.UUID}, Name: {product.Name}, Category: {product.Category}, Price: {product.Price}");
    }

    public static void RenderProductList(List<Product> products)
    {
        Console.WriteLine("Product List:");
        foreach (var product in products)
        {
            Console.WriteLine($"UUID: {product.UUID}, Name: {product.Name}, Category: {product.Category}, Price: {product.Price}");
        }
    }

    public static void RenderMessage(string message)
    {
        Console.WriteLine(message);
    }
}
