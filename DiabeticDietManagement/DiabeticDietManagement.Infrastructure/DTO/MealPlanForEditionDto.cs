using System;
using System.Collections.Generic;
using System.Text;

namespace DiabeticDietManagement.Infrastructure.DTO
{
    public class MealPlanForEditionDto
    {
        public string Name { get; set; }
        public HashSet<DailyPlanForEditionDto> DailyPlans { get; set; }
    }
}
