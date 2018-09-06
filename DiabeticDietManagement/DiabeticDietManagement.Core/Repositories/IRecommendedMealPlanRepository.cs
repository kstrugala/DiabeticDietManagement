using DiabeticDietManagement.Core.Domain;
using DiabeticDietManagement.Core.Helpers;
using DiabeticDietManagement.Core.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiabeticDietManagement.Core.Repositories
{
    public interface IRecommendedMealPlanRepository : IRepository
    {
        Task<RecommendedMealPlan> GetAsync(Guid id);
        Task AddAsync(RecommendedMealPlan plan);
        Task UpdateAsync(RecommendedMealPlan plan);
        Task RemoveAsync(Guid id);

    }
}
