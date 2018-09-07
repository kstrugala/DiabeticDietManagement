using DiabeticDietManagement.Core.Domain;
using DiabeticDietManagement.Infrastructure.Commands.RecommendedMealPlan;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Plan Id</param>
        /// <param name="command"></param>
        /// <returns></returns>
        Task UpdateMealPlan(Guid id, UpdateRecommendedMealPlan command);
        Task RemoveMealPlan(Guid id);
    }
}
