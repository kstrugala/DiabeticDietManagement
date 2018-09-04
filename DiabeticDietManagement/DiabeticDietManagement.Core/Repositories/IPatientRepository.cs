using DiabeticDietManagement.Core.Domain;
using DiabeticDietManagement.Core.Helpers;
using DiabeticDietManagement.Core.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiabeticDietManagement.Core.Repositories
{
    public interface IPatientRepository : IRepository
    {
        Task<Patient> GetAsync(Guid Id);
        Task<IEnumerable<Patient>> GetAllAsync();
        Task<PagedResult<Patient>> GetPatientsAsync(PatientQuery query);
        Task AddAsync(Patient patient);
        Task UpdateAsync(Patient patient);
        Task RemoveAsync(Guid Id);
    }
}
