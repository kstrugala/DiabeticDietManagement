using DiabeticDietManagement.Infrastructure.Commands.DietaryCompliance;
using DiabeticDietManagement.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiabeticDietManagement.Infrastructure.Services
{
    public interface IDietaryComplianceService : IService
    {
        Task<IEnumerable<DietaryComplianceDto>> GetPatientDietaryComplianceAsync(Guid patientId);
        Task AddPatientDietaryComplianceAsync(AddDietaryCompliance command);
        Task RemovePatientDietaryComplianceAsync(Guid patientId);
    }
}
