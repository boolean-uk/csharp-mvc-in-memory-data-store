using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;
using exercise.wwwapi.Helpers;

namespace exercise.wwwapi.Services
{
    public class ProductsService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IdGenerator _idGenerator;

        public ProductsService(IRepository<Product> repository, IdGenerator idGenerator) 
        {
            _productRepository = repository;
            _idGenerator = idGenerator;
        }

        public Product Create(ProductDTO productDTO)
        {
            var products = _productRepository.GetAll();

            if (products.Any(p => p.Name == productDTO.Name))
                return null;

            var product = new Product
            {
                Id = _idGenerator.GetNextId(),
                Name = productDTO.Name,
                Category = productDTO.Category,
                Price = productDTO.Price
            };

            return _productRepository.Create(product);
        }

        public List<Product> GetAll(string? category)
        {

            var products = _productRepository.GetAll();

            //No category provided
            if (string.IsNullOrWhiteSpace(category))
                return products;

            products = products.FindAll(p => p.Category.ToLower() == category.ToLower());

            //No category found
            if (products.Count() == 0)
                return null;

            return products;
        }

        public Product Get(int id)
        {
            return _productRepository.Get(id);
        }

        public Product Update(int id, ProductDTO productDTO)
        {
            var products = _productRepository.GetAll();

            if (products.Any(p => p.Name == productDTO.Name))
                return null;

            var product = new Product
            {
                Id = id,
                Name = productDTO.Name,
                Category = productDTO.Category,
                Price = productDTO.Price
            };

            return _productRepository.Update(id, product);
        }

        public Product Delete(int id)
        {
            return (_productRepository.Delete(id));
        }
    }
}
