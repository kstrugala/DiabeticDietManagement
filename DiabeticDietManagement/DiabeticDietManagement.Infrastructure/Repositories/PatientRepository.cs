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
    public class PatientRepository : IPatientRepository
    {
        private readonly DiabeticDietContext _context;

        public PatientRepository(DiabeticDietContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Patient>> GetAllAsync()
            => await _context.Patients.ToListAsync();

        public async Task<Patient> GetAsync(Guid Id)
            => await _context.Patients.SingleOrDefaultAsync(x => x.UserId == Id);

        public async Task<PagedResult<Patient>> GetPatientsAsync(PatientQuery query)
        {
            var page = query.Page;
            var pageSize = query.PageSize;

            // Filter
            var linqQuery = _context.Patients
                                    .Where(x => x.FirstName.Contains(query.FirstName) && x.LastName.Contains(query.LastName));


            var count = await linqQuery.CountAsync();

            var totalPages = (int)Math.Ceiling(count / (double)pageSize);

            if (page < 1) page = 1;

            if (totalPages == 0) totalPages = 1;

            if (page > totalPages) page = totalPages;

            var results = await linqQuery
                                    .Skip((page - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();

            return new PagedResult<Patient>(results, count, page, pageSize, totalPages);
        }

        public async Task AddAsync(Patient patient)
        {
            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Patient patient)
        {
            _context.Patients.Update(patient);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid Id)
        {
            var patient = await GetAsync(Id);
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
        }
        
    }
}
