﻿using ByteStore.Application.Services.Interfaces;
using ByteStore.Domain.Entities;
using ByteStore.Infrastructure.Repository.Interfaces;
using ByteStore.Shared.DTO;

namespace ByteStore.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _productRepository.GetAllProducts();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _productRepository.GetProductById(id);
        }

        public async Task<Product> AddProduct(ProductDto productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                ProductQuantity = productDto.ProductQuantity,
                Price = productDto.Price,
            };
            return await _productRepository.AddProduct(product);
        }

        public async Task<Product> UpdateProduct(int id, ProductDto productDto)
        {

            var product = new Product
            {
                Name = productDto.Name,
                ProductQuantity = productDto.ProductQuantity,
                Price = productDto.Price,
            };
            return await _productRepository.UpdateProduct(id, product);
        }

        public async Task<bool> DeleteProduct(int id)
        {
            return await _productRepository.DeleteProduct(id);
        }
    }
}