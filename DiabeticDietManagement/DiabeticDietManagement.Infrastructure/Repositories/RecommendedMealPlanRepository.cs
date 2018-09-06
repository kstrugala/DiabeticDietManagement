using DiabeticDietManagement.Core.Domain;
using DiabeticDietManagement.Core.Helpers;
using DiabeticDietManagement.Core.Queries;
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
    public class RecommendedMealPlanRepository : IRecommendedMealPlanRepository
    {
        private readonly DiabeticDietContext _context;

        public RecommendedMealPlanRepository(DiabeticDietContext context)
        {
            _context = context;
        }

        public async Task AddAsync(RecommendedMealPlan plan)
        {
            await _context.RecommendedMealPlans.AddAsync(plan);
            await _context.SaveChangesAsync();
        }

        public async Task<RecommendedMealPlan> GetAsync(Guid id)
            => await _context.RecommendedMealPlans.SingleOrDefaultAsync(x => x.Id == id);

        public async Task RemoveAsync(Guid id)
        {
            var plan = await GetAsync(id);
            if (plan == null)
                throw new DomainException(ErrorCodes.InvalidId ,$"Cannot find plan with id:{id}");
            _context.RecommendedMealPlans.Remove(plan);
            await _context.SaveChangesAsync();

        }

        public async Task UpdateAsync(RecommendedMealPlan plan)
        {
            _context.RecommendedMealPlans.Update(plan);
            await _context.SaveChangesAsync();
        }
    }
}
