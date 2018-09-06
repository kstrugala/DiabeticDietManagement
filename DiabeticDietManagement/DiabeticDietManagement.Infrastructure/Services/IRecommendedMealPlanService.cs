using DiabeticDietManagement.Core.Domain;
using DiabeticDietManagement.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiabeticDietManagement.Infrastructure.Services
{
    public interface IRecommendedMealPlanService : IService
    {
        Task<RecommendedMealPlanDto> GetMealPlan(Guid id);
        Task CreateMealPlan(RecommendedMealPlan plan);
        Task UpdateMealPlan(RecommendedMealPlan plan);
        Task RemoveMealPlan(Guid id);
    }
}
