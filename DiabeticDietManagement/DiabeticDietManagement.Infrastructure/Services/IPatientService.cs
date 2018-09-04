using DiabeticDietManagement.Core.Helpers;
using DiabeticDietManagement.Core.Queries;
using DiabeticDietManagement.Infrastructure.Commands.Patients;
using DiabeticDietManagement.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiabeticDietManagement.Infrastructure.Services
{
    public interface IPatientService : IService
    {
        Task<PatientDto> GetAsync(string email);
        Task<PagedResult<PatientDto>> BrowseAsync(PatientQuery query);
        Task CreateAsync(CreatePatient patient);
        Task UpdateAsync(UpdatePatient patient);
        Task RemoveAsync(string email);
    }
}
