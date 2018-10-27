using DiabeticDietManagement.Infrastructure.Commands.RecommendedMealPlan.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiabeticDietManagement.Infrastructure.Commands.RecommendedMealPlan
{
    public class UpdateRecommendedMealPlan : ICommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ISet<DailyMealPlanForSave> DailyPlans { get; set; }

    }
}
