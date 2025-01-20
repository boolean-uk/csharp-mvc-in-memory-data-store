public class ProductsRepository
{
    private readonly ProductsData _productsData;

    public ProductsRepository(ProductsData productsData)
    {
        _productsData = productsData;
    }

    public async Task<Product> AddAsync(Product product)
    {
        return await _productsData.AddAsync(product);
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _productsData.GetAllAsync();
    }

    public async Task<Product> GetAsync(Guid UUID)
    {
        return await _productsData.GetAsync(UUID);
    }

    public async Task<Product> UpdateAsync(Guid UUID, string? name, string? category, int? price)
    {
        return await _productsData.UpdateAsync(UUID, name, category, price);
    }

    public async Task<bool> DeleteAsync(Guid UUID)
    {
        return await _productsData.DeleteAsync(UUID);
    }
}
