using DiabeticDietManagement.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiabeticDietManagement.Core.Repositories
{
    public interface IDietaryComplianceRepository : IRepository
    {
        Task<DietaryCompliance> GetOneAsync(Guid id);
        Task<IEnumerable<DietaryCompliance>> GetAsync(Guid patientId);
        Task AddAsync(DietaryCompliance dietaryCompliance);
        Task UpdateAsync(DietaryCompliance dietaryCompliance);
        Task RemoveAsync(Guid id);
        Task RemovePatientDietaryComplianceAsync(Guid patientId);
    }
}
