using System;
using System.Collections.Generic;
using System.Text;

namespace DiabeticDietManagement.Core.Domain
{
    public class DailyMealPlan
    {
        public Guid Id { get; protected set; }
        public uint Day { get; protected set; }
        public Meal Breakfast { get; protected  set; }
        public Meal Snap { get; protected  set; }
        public Meal Lunch { get; protected set; }
        public Meal Dinner { get; protected set; }
        public Meal Supper { get; protected set; }

        protected DailyMealPlan()
        {

        }

        public DailyMealPlan(uint day, Meal breakfast, Meal snap, Meal lunch, Meal dinner, Meal supper)
        {
            Id = Guid.NewGuid();
            Day = day;
            SetBreakfast(breakfast);
            SetSnap(snap);
            SetLunch(lunch);
            SetDinner(dinner);
            SetSupper(supper);

        }

        public void SetBreakfast(Meal breakfast)
        {
            Breakfast = breakfast;
        }

        
        public void SetSnap(Meal snap)
        {
            Snap = snap;
        }

        public void SetLunch(Meal lunch)
        {
            Lunch = lunch;
        }

        public void SetDinner(Meal dinner)
        {
            Dinner = dinner;
        }

        public void SetSupper(Meal supper)
        {
            Supper = supper;
        }
    }
}
