using DiabeticDietManagement.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiabeticDietManagement.Infrastructure.Commands.RecommendedMealPlan
{
    public class SaveRecommendedMealPlan
    {
        public string Name { get; set; }
        public ISet<DailyMealPlan> DailyPlans { get; set; }
    }
}
