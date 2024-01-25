using exercise.wwwapi.Model;
using exercise.wwwapi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;

        public ProductsController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _repository.GetProducts();
        }

        [HttpGet("{id}")]
        public async Task<Product> GetProduct(int id)
        {
            return await _repository.GetProduct(id);
        }

        [HttpPost]
        public async Task<Product> CreateProduct(Product product)
        {
            return await _repository.AddProduct(product);
        }

        [HttpPut("{id}")]
        public async Task<Product> UpdateProduct(int id , Product product)
        {
            return await _repository.UpdateProduct(id , product);
        }

        [HttpDelete("{id}")]
        public async Task<Product> DeleteProduct(int id)
        {
            return await _repository.DeleteProduct(id);
        }
    }
}
