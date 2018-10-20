using DiabeticDietManagement.Core.Helpers;
using DiabeticDietManagement.Core.Queries;
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
        Task<PagedResult<DietaryComplianceDto>> GetPatientDietaryCompliancePagedAsync(Guid patientId, DietaryComplianceQuery query);
        Task AddPatientDietaryComplianceAsync(AddDietaryCompliance command);
        Task RemovePatientDietaryComplianceAsync(Guid patientId);
    }
}
