using DiabeticDietManagement.Core.Domain;
using DiabeticDietManagement.Core.Helpers;
using DiabeticDietManagement.Core.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiabeticDietManagement.Core.Repositories
{
    public interface IProductRepository : IRepository
    {
        Task<Product> GetAsync(Guid Id);
        Task<Product> GetAsync(string productName);
        Task<PagedResult<Product>> GetProductsAsync(ProductQuery query);
        Task<IEnumerable<Product>> GetAllAsync();
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
    }
}
