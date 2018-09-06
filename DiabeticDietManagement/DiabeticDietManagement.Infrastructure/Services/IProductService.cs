using DiabeticDietManagement.Core.Helpers;
using DiabeticDietManagement.Core.Queries;
using DiabeticDietManagement.Infrastructure.Commands.Products;
using DiabeticDietManagement.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiabeticDietManagement.Infrastructure.Services
{
    public interface IProductService : IService
    {
        Task<ProductDto> GetAsync(Guid Id);
        Task<ProductDto> GetAsync(string productName);
        Task<PagedResult<ProductDto>> BrowseAsync(ProductQuery query);
        Task CreateAsync(CreateProduct product);
        Task UpdateAsync(UpdateProduct product);
    }
}
