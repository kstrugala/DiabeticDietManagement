using System;
using System.Collections.Generic;
using System.Text;

namespace DiabeticDietManagement.Infrastructure.Commands.RecommendedMealPlan.Helpers
{
    public class DailyMealPlanForSave
    {
        public uint Day { get; set; }
        public MealForSave Breakfast { get; set; }
        public MealForSave Snap { get; set; }
        public MealForSave Lunch { get; set; }
        public MealForSave Dinner { get; set; }
        public MealForSave Supper { get; set; }
    }
}
