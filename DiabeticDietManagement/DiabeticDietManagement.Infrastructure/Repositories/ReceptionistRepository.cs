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
    public class ReceptionistRepository : IReceptionistRepository
    {
        DiabeticDietContext _context;

        public ReceptionistRepository(DiabeticDietContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Receptionist receptionist)
        {
            await _context.Receptionists.AddAsync(receptionist);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Receptionist>> GetAllAsync()
            => await _context.Receptionists.ToListAsync();

        public async Task<Receptionist> GetAsync(Guid Id)
            => await _context.Receptionists.SingleOrDefaultAsync(x => x.UserId == Id);

        public async Task<PagedResult<Receptionist>> GetReceptionistsAsync(ReceptionistQuery query)
        {
            var page = query.Page;
            var pageSize = query.PageSize;

            // Filter
            var linqQuery = _context.Receptionists
                                    .Where(x => x.FirstName.Contains(query.FirstName) || x.LastName.Contains(query.LastName));


            var count = await linqQuery.CountAsync();

            var totalPages = (int)Math.Ceiling(count / (double)pageSize);

            if (page < 1) page = 1;

            if (totalPages == 0) totalPages = 1;

            if (page > totalPages) page = totalPages;

            var results = await linqQuery
                                    .Skip((page - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();

            return new PagedResult<Receptionist>(results, count, page, pageSize, totalPages);
        }

        public async Task UpdateAsync(Receptionist receptionist)
        {
            _context.Receptionists.Update(receptionist);
            await _context.SaveChangesAsync();
        }
    }
}
