using DiabeticDietManagement.Core.Domain;
using DiabeticDietManagement.Core.Helpers;
using DiabeticDietManagement.Core.Queries;
using DiabeticDietManagement.Core.Repositories;
using DiabeticDietManagement.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiabeticDietManagement.Infrastructure.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        DiabeticDietContext _context;

        public DoctorRepository(DiabeticDietContext context)
        {
                _context = context;
        }

        public async Task<Doctor> GetAsync(Guid Id)
            => await _context.Doctors.SingleOrDefaultAsync(x => x.UserId == Id);

           
        public async Task<IEnumerable<Doctor>> GetAllAsync()
            => await _context.Doctors.ToListAsync();

        public async Task AddAsync(Doctor doctor)
        {
            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Doctor doctor)
        {
            _context.Doctors.Update(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task<PagedResult<Doctor>> GetDoctorsAsync(DoctorQuery query)
        {
            var page = query.Page;
            var pageSize = query.PageSize;

            // Filter
            var linqQuery = _context.Doctors
                                    .Where(x => x.FirstName.Contains(query.FirstName) && x.LastName.Contains(query.LastName));
                                    

            var count = await linqQuery.CountAsync();

            var totalPages = (int)Math.Ceiling(count / (double)pageSize);

            if (page < 1) page = 1;

            if (totalPages == 0) totalPages = 1;

            if (page > totalPages) page = totalPages;

            var results = await linqQuery
                                    .Skip((page-1)*pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();

            return new PagedResult<Doctor>(results, count, page, pageSize, totalPages);
        }
    }
}
