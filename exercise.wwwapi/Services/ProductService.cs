using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;
using exercise.wwwapi.Helpers;

namespace exercise.wwwapi.Services
{
    public class ProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IdGenerator _idGenerator;

        public ProductService(IRepository<Product> repository, IdGenerator idGenerator) 
        {
            _productRepository = repository;
            _idGenerator = idGenerator;
        }

        public Product Create(ProductDTO productDTO)
        {
            var product = new Product
            {
                Id = _idGenerator.GetNextId(),
                Name = productDTO.Name,
                Category = productDTO.Category,
                Price = productDTO.Price
            };

            return _productRepository.Create(product);
        }

        public List<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public Product Get(int id)
        {
            return _productRepository.Get(id);
        }

        public Product Update(int id, ProductDTO productDTO)
        {
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
