using System;
using System.Collections.Generic;
using System.Text;

namespace DiabeticDietManagement.Core.Domain
{
    public class MealPlan
    {
        private ISet<DailyMealPlan> _dailyMealPlans = new HashSet<DailyMealPlan>();

        public string Name { get; protected set; }
         
        public ISet<DailyMealPlan> DailyPlans {
            get { return _dailyMealPlans; }
            set { _dailyMealPlans = new HashSet<DailyMealPlan>(value); }
        }

        protected MealPlan()
        {

        }

        public MealPlan(string name)
        {
            SetName(name);
        }

        public void SetName(string name)
        {
            if (!String.IsNullOrWhiteSpace(name))
                Name = name;
            else
                throw new DomainException("Invalid name");
        }
    }
}
