using DiabeticDietManagement.Core.Domain;
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
    }
}
