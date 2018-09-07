using System;
using System.Collections.Generic;
using System.Text;

namespace DiabeticDietManagement.Infrastructure.Commands.RecommendedMealPlan.Helpers
{
    public class DailyMealPlan
    {
        public uint Day { get; set; }
        public Meal Breakfast { get; set; }
        public Meal Snap { get; set; }
        public Meal Lunch { get; set; }
        public Meal Dinner { get; set; }
        public Meal Supper { get; set; }
    }
}
