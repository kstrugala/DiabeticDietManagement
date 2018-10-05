using System;
using System.Collections.Generic;
using System.Text;

namespace DiabeticDietManagement.Infrastructure.DTO
{
    public class DailyPlanForEditionDto
    {
        public uint Day { get; set; }
        public MealForEditionDto Breakfast { get; set; }
        public MealForEditionDto Snap { get; set; }
        public MealForEditionDto Lunch { get; set; }
        public MealForEditionDto Dinner { get; set; }
        public MealForEditionDto Supper { get; set; }
    }
}
