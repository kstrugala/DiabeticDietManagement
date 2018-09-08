using System;
using System.Collections.Generic;
using System.Text;

namespace DiabeticDietManagement.Core.Domain
{
    public class DailyMealPlan
    {
        public uint Day { get; set; }
        public Meal Breakfast { get; set; }
        public Meal Snap { get; set; }
        public Meal Lunch { get; set; }
        public Meal Dinner { get; set; }
        public Meal Supper { get; set; }

        public DailyMealPlan()
        {
            Breakfast = new Meal();
            Snap = new Meal();
            Lunch = new Meal();
            Dinner = new Meal();
            Supper = new Meal();
        }

    }
}
