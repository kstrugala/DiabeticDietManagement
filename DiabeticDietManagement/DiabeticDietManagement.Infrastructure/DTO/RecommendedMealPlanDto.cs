using DiabeticDietManagement.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiabeticDietManagement.Infrastructure.DTO
{
    public class RecommendedMealPlanDto
    {
        public string Name { get; set; }
        public ISet<DailyMealPlan> DailyMealPlans { get; set; }
    }
}
