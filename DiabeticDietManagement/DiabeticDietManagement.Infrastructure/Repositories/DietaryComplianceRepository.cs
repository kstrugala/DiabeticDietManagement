using DiabeticDietManagement.Core.Domain;
using DiabeticDietManagement.Core.Domain.Enums;
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
    public class DietaryComplianceRepository : IDietaryComplianceRepository
    {
        private DiabeticDietContext _context;

        public DietaryComplianceRepository(DiabeticDietContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DietaryCompliance>> GetAsync(Guid patientId)
            => await _context.DietaryCompliances
                           .Where(x => x.PatientId == patientId)
                           .OrderByDescending(y => y.Date)
                           .ToListAsync();


        public async Task AddAsync(DietaryCompliance dietaryCompliance)
        {
            await _context.DietaryCompliances.AddAsync(dietaryCompliance);
            await _context.SaveChangesAsync();
        }
        public async Task RemoveAsync(Guid id)
        {
            var d = await GetOneAsync(id);
            _context.Remove(d);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(DietaryCompliance dietaryCompliance)
        {
            _context.DietaryCompliances.Update(dietaryCompliance);
            await _context.SaveChangesAsync();
        }

        public async Task RemovePatientDietaryComplianceAsync(Guid patientId)
        {
            var dc = await GetAsync(patientId);
            foreach (var i in dc)
            {
                _context.DietaryCompliances.Remove(i);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<DietaryCompliance> GetOneAsync(Guid id)
           => await _context.DietaryCompliances.SingleOrDefaultAsync(x => x.Id == id);

        public async Task<DietaryCompliance> GetAsync(Guid patientId, DateTime date, MealType mealType)
            => await _context.DietaryCompliances.SingleOrDefaultAsync(x => x.PatientId == patientId && x.MealType == mealType && x.Date.Date.Equals(date.Date));

        public async Task<PagedResult<DietaryCompliance>> GetPagedAsync(Guid patientId, DietaryComplianceQuery query)
        {
            var page = query.Page;
            var pageSize = query.PageSize;

            // Filter
            var linqQuery = _context.DietaryCompliances
                           .Where(x => x.PatientId == patientId);


            var count = await linqQuery.CountAsync();

            var totalPages = (int)Math.Ceiling(count / (double)pageSize);

            if (page < 1) page = 1;

            if (totalPages == 0) totalPages = 1;

            if (page > totalPages) page = totalPages;


            var results = await linqQuery
                                    .OrderByDescending(y => y.Date)
                                    .Skip((page - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();

            return new PagedResult<DietaryCompliance>(results, count, page, pageSize, totalPages);
        }
    }
}