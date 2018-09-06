using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DiabeticDietManagement.Core.Domain;
using DiabeticDietManagement.Core.Repositories;
using DiabeticDietManagement.Infrastructure.DTO;
using DiabeticDietManagement.Infrastructure.Exceptions;

namespace DiabeticDietManagement.Infrastructure.Services
{
    public class RecommendedMealPlanService : IRecommendedMealPlanService
    {
        private readonly IRecommendedMealPlanRepository _repository;
        private readonly IMapper _mapper;

        public RecommendedMealPlanService(IMapper mapper, IRecommendedMealPlanRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task CreateMealPlan(RecommendedMealPlan plan)
        {
            await _repository.AddAsync(plan);
        }

        public async Task<RecommendedMealPlanDto> GetMealPlan(Guid id)
        {
            var plan = await _repository.GetAsync(id);
            if (plan == null)
                throw new ServiceException(ErrorCodes.InvalidId, $"Plan with id:{id} doesn't exist.");
            return _mapper.Map<RecommendedMealPlan, RecommendedMealPlanDto>(plan);
        }

        public async Task RemoveMealPlan(Guid id)
        {
            var plan = await _repository.GetAsync(id);
            if (plan == null)
                throw new ServiceException(ErrorCodes.InvalidId, $"Plan with id:{id} doesn't exist.");
            await _repository.RemoveAsync(id);
        }

        public async Task UpdateMealPlan(RecommendedMealPlan plan)
        {
            await _repository.UpdateAsync(plan);
        }
    }
}
