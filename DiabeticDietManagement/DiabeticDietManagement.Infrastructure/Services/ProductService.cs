using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DiabeticDietManagement.Core.Domain;
using DiabeticDietManagement.Core.Helpers;
using DiabeticDietManagement.Core.Queries;
using DiabeticDietManagement.Core.Repositories;
using DiabeticDietManagement.Infrastructure.Commands.Products;
using DiabeticDietManagement.Infrastructure.DTO;
using DiabeticDietManagement.Infrastructure.Exceptions;

namespace DiabeticDietManagement.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<PagedResult<ProductDto>> BrowseAsync(ProductQuery query)
        {
            var repositoryResults = await _productRepository.GetProductsAsync(query);
            var result = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(repositoryResults.Results);


            return new PagedResult<ProductDto>(result, repositoryResults.Pagination.TotalCount,
                                              repositoryResults.Pagination.CurrentPage, repositoryResults.Pagination.PageSize,
                                              repositoryResults.Pagination.TotalPages);
        }

        public async Task CreateAsync(CreateProduct product)
        {
            if(String.IsNullOrWhiteSpace(product.ProductName))
            {
                throw new ServiceException(ErrorCodes.InvalidProductName, "Product name cannot be empty.");
            }
            
            var p = new Product(product.Id, product.ProductName, product.GlycemicIndex, product.GlycemicLoad, product.ServeSize, product.Carbohydrates);
            await _productRepository.AddAsync(p);
        }
        public async Task<ProductDto> GetAsync(Guid Id)
        {
            var product = await _productRepository.GetAsync(Id);
            return _mapper.Map<Product, ProductDto>(product);
        }
        public async Task<ProductDto> GetAsync(string productName)
        {
            var product = await _productRepository.GetAsync(productName);
            return _mapper.Map<Product, ProductDto>(product);
        }

        public async Task UpdateAsync(UpdateProduct product)
        {
            if(product.Id==null)
            {
                throw new ServiceException(ErrorCodes.InvalidId, $"Product ID:{product.Id} cannot be empty.");
            }

            var p = await _productRepository.GetAsync(product.Id);
            if(p==null)
            {
                throw new ServiceException(ErrorCodes.InvalidId, $"Product with ID:{product.Id} doesn't exist.");
            }

            p.SetProductName(product.ProductName);
            p.SetGlycemicIndex(product.GlycemicIndex);
            p.SetGlycemicLoad(product.GlycemicLoad);
            p.SetCarbohydrates(product.Carbohydrates);

            await _productRepository.UpdateAsync(p);
        }
    }
}
