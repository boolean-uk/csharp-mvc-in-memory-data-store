public class ProductsRepository
{
    private readonly ProductsData _productsData;

    public ProductsRepository(ProductsData productsData) { _productsData = productsData; }

    public Product Add(Product product) { return _productsData.Add(product); }

    public List<Product> GetAll() { return _productsData.GetAll(); }

    public Product Get(Guid UUID) {  return _productsData.Get(UUID); }

    public Product Update(Guid UUID, string name, string category, int price)
    {
        return _productsData.Update(UUID, name, category, price);
    }

    public bool Delete(Guid UUID) { return _productsData.Delete(UUID); }
}
