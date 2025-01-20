
public class ProductsData
{
    private List<Product> _products = new List<Product>() 
    {
        new Product("Bagel", "Food", 1),
        new Product("Coffee", "Drink", 2)
    };

    public Product Add(Product product)
    {
        _products.Add(product);
        return product;
    }

    public List<Product> GetAll()
    {
        return _products.ToList();
    }

    public Product Get(Guid UUID)
    {
        return _products.FirstOrDefault(p => p.UUID == UUID);
    }

    public Product Update(Guid UUID, string? name, string? category, int? price)
    {
        Product product = _products.FirstOrDefault(p => p.UUID == UUID);
        if (!string.IsNullOrEmpty(name))
        {
            product.Name = name;
        }
        if (!string.IsNullOrEmpty(category))
        {
            product.Category = category;
        }
        if (price != null && price != product.Price && price > 0)
        {
            product.Price = (int)price;
        }
        return product;
    }

    public bool Delete(Guid UUID)
    {
        Product product = _products.FirstOrDefault(p => p.UUID == UUID);
        if (product != null)
        {
            return _products.Remove(product);
        }
        return false;
    }
}
