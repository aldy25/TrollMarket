using TrollMarket.Business.Interface;
using TrollMarket.DataAcces.Models;
using TrollMarket.Presentation.Api.ModelDto.Product;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TrollMarket.Presentation.Api.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IOrderRepository _orderRepository;
        public ProductService(IProductRepository productRepository, ICartRepository cartRepository, IOrderRepository orderRepository)
        {
            _productRepository = productRepository;
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
        }

        public ProductModelDto Insert(ProductModelDto dto)
        {
            Product product = new Product
            {
                ProductName = dto.ProductName,
                Category = dto.Category,
                Description = dto.Description,
                Price = dto.Price,
                Discontinue = dto.Discontinue,
                SellerNumber = dto.SellerNumber
            };
            var result = _productRepository.Insert(product);
            return new ProductModelDto
            {
                Id = result.Id,
                ProductName = result.ProductName,
                Category = result.Category,
                Description = result.Description,
                Price = result.Price,
                Discontinue = result.Discontinue,
                SellerNumber = result.SellerNumber
            };
        }
        public ProductModelDto Update(ProductModelDto dto)
        {
            Product product = new Product
            {
                Id = dto.Id,
                ProductName = dto.ProductName,
                Category = dto.Category,
                Description = dto.Description,
                Price = dto.Price,
                Discontinue = dto.Discontinue,
                SellerNumber = dto.SellerNumber
            };
            var result = _productRepository.Update(product);
            return new ProductModelDto
            {
                Id = result.Id,
                ProductName = result.ProductName,
                Category = result.Category,
                Description = result.Description,
                Price = result.Price,
                Discontinue = result.Discontinue,
                SellerNumber = result.SellerNumber
            };
        }
        public bool CheckAvailableUpdateProduct(int id)
        {
            if (_cartRepository.CountCartByProductId(id) > 0)
            {
                return false;
            }else if (_orderRepository.CountOrderByProductId(id) > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public ProductModelDto Delete(int id)
        {
            Product result = _productRepository.GetProductById(id)
                ?? throw new KeyNotFoundException($"roduct with id {id} not found");
            if (_cartRepository.CountCartByProductId(id)>0)
            {
                throw new InvalidOperationException("The product cannot be deleted because it has already been used");
            }else if (_orderRepository.CountOrderByProductId(id)>0)
            {
                throw new InvalidOperationException("The product cannot be deleted because it has already been used");
            }
            else
            {
                _productRepository.Delete(result);
                return new ProductModelDto
                {
                    Id = result.Id,
                    ProductName = result.ProductName,
                    Category = result.Category,
                    Description = result.Description,
                    Price = result.Price,
                    Discontinue = result.Discontinue,
                    SellerNumber = result.SellerNumber
                };
            }
        }
        public ProductModelDto ProductDiscontinue(int id) 
        {
            Product result = _productRepository.GetProductById(id)
                ?? throw new KeyNotFoundException("roduct with id {id} not found");
            result.Discontinue = true;
            _productRepository.Update(result);
            return new ProductModelDto
            {
                Id = result.Id,
                ProductName = result.ProductName,
                Category = result.Category,
                Description = result.Description,
                Price = result.Price,
                Discontinue = result.Discontinue,
                SellerNumber = result.SellerNumber
            };
        }
        public ProductModelDto GetProductById(int id)
        {
            Product result = _productRepository.GetProductById(id)
                ?? throw new KeyNotFoundException("roduct with id {id} not found");
            return new ProductModelDto
            {
                Id = result.Id,
                ProductName = result.ProductName,
                Category = result.Category,
                Description = result.Description,
                Price = result.Price,
                Discontinue = result.Discontinue,
                SellerNumber = result.SellerNumber
            };
        }
    }
}
