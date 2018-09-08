using System;
using System.Collections.Generic;
using System.Text;

namespace DiabeticDietManagement.Infrastructure.DTO
{
    public class MealPlanDto
    {
        public string Name { get; set; }
        public HashSet<DailyPlanDto> DailyPlans { get; set; }
    }
}
