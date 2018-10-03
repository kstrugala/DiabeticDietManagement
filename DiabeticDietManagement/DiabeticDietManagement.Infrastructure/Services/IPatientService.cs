using DiabeticDietManagement.Core.Domain;
using DiabeticDietManagement.Core.Helpers;
using DiabeticDietManagement.Core.Queries;
using DiabeticDietManagement.Infrastructure.Commands.Patients;
using DiabeticDietManagement.Infrastructure.Commands.RecommendedMealPlan;
using DiabeticDietManagement.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiabeticDietManagement.Infrastructure.Services
{
    public interface IPatientService : IService
    {
        Task<PatientDto> GetAsync(Guid id);
        Task<PatientDto> GetAsync(string email);
        Task<PagedResult<PatientDto>> BrowseAsync(PatientQuery query);
        Task CreateAsync(CreatePatient patient);
        Task UpdateAsync(UpdatePatient patient);
        Task RemoveAsync(Guid id);
        Task RemoveAsync(string email);

        Task<MealPlanDto> GetMealPlanAsync(Guid id);
        Task<MealPlan> GetMealPlanForEditionAsync(Guid id);

        Task UpdateMealPlanAsync(UpdateRecommendedMealPlan command);


    }
}
