using DiabeticDietManagement.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiabeticDietManagement.Infrastructure.DTO
{
    public class RecommendedMealPlanDto
    {
        public string Name { get; set; }
        public IEnumerable<DailyMealPlan> DailyMealPlans { get; set; }
    }
}
