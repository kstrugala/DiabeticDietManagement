using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DiabeticDietManagement.Core.Domain;
using DiabeticDietManagement.Core.Repositories;
using DiabeticDietManagement.Infrastructure.Commands.RecommendedMealPlan;
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

        public async Task UpdateMealPlan(Guid  planId, UpdateRecommendedMealPlan command)
        {
            var plan = await _repository.GetAsync(planId);

            if (!String.IsNullOrWhiteSpace(command.Name))
                plan.SetName(command.Name);
            else
                throw new ServiceException(ErrorCodes.InvalidPlanName, "Plan name cannot be empty.");

            foreach (var dailyPlan in command.DailyPlans)
            {
                var breakfast = new Meal();
                var snap = new Meal();
                var lunch = new Meal();
                var dinner = new Meal();
                var supper = new Meal();

                foreach (var portion in dailyPlan.Breakfast.Products)
                {
                    breakfast.AddPortion(new Portion(portion.ProductId, portion.Quantity));
                }

                foreach (var portion in dailyPlan.Snap.Products)
                {
                    snap.AddPortion(new Portion(portion.ProductId, portion.Quantity));
                }

                foreach (var portion in dailyPlan.Lunch.Products)
                {
                    lunch.AddPortion(new Portion(portion.ProductId, portion.Quantity));
                }

                foreach (var portion in dailyPlan.Dinner.Products)
                {
                    dinner.AddPortion(new Portion(portion.ProductId, portion.Quantity));
                }

                foreach (var portion in dailyPlan.Supper.Products)
                {
                    supper.AddPortion(new Portion(portion.ProductId, portion.Quantity));
                }

                var dailyMealPlan = new DailyMealPlan(dailyPlan.Day, breakfast, snap, lunch, dinner, supper);

                plan.UpdateDailyMealPlan(dailyMealPlan);
            }

            await _repository.UpdateAsync(plan);
        }
    }
}
