using DiabeticDietManagement.Core.Domain;
using DiabeticDietManagement.Core.Helpers;
using DiabeticDietManagement.Core.Queries;
using DiabeticDietManagement.Core.Repositories;
using DiabeticDietManagement.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabeticDietManagement.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DiabeticDietContext _context;

        public ProductRepository(DiabeticDietContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAllAsync() 
            => await _context.Products.ToListAsync();


        public async Task<Product> GetAsync(Guid Id) 
            => await _context.Products.SingleOrDefaultAsync(x=>x.Id==Id);

        public async Task<Product> GetAsync(string productName)
            => await _context.Products.SingleOrDefaultAsync(x => x.ProductName == productName);

        public async Task<PagedResult<Product>> GetProductsAsync(ProductQuery query)
        {
            var page = query.Page;
            var pageSize = query.PageSize;

            // Filter

            var linqQuery = _context.Products
                                .Where(x => x.ProductName.Contains(query.ProductName));


            var count = await linqQuery.CountAsync();

            var totalPages = (int)Math.Ceiling(count / (double)pageSize);

            if (page < 1) page = 1;

            if (totalPages == 0) totalPages = 1;

            if (page > totalPages) page = totalPages;

            var results = await linqQuery
                                    .Skip((page - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();

            return new PagedResult<Product>(results, count, page, pageSize, totalPages);
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}
