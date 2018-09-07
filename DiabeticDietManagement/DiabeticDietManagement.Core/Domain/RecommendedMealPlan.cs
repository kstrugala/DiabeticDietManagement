using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiabeticDietManagement.Core.Domain
{
    public class RecommendedMealPlan
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        //public Guid DoctorId { get; protected set; }

        private ISet<DailyMealPlan> _dailyMealPlans = new HashSet<DailyMealPlan>();


        public IEnumerable<DailyMealPlan> DailyMealPlans
        {
            get { return _dailyMealPlans; }
            set { _dailyMealPlans = new HashSet<DailyMealPlan>(); ; }
        }

        protected RecommendedMealPlan()
        {

        }

        public RecommendedMealPlan(string name)
        {
            Id = Guid.NewGuid();
            SetName(name);
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void AddDailyMealPlan(DailyMealPlan dailyMealPlan)
        {
            var d = DailyMealPlans.SingleOrDefault(x => x.Day == dailyMealPlan.Day);

            if (d != null)
            {
                throw new DomainException(ErrorCodes.InvalidPortion, $"Daily meal plan already exists.");
            }
            _dailyMealPlans.Add(dailyMealPlan);
        }

        public void UpdateDailyMealPlan(DailyMealPlan dailyMealPlan)
        {
            var d = DailyMealPlans.SingleOrDefault(x => x.Day == dailyMealPlan.Day);

            if (d == null)
            {
                AddDailyMealPlan(dailyMealPlan);
            }
            else
            {
                d.SetBreakfast(dailyMealPlan.Breakfast);
                d.SetSnap(dailyMealPlan.Snap);
                d.SetLunch(dailyMealPlan.Lunch);
                d.SetDinner(dailyMealPlan.Dinner);
                d.SetSupper(dailyMealPlan.Supper);
            }
        }

        public void RemoveDailyMealPlan(DailyMealPlan dailyMealPlan)
        {
            var d = DailyMealPlans.SingleOrDefault(x => x.Equals(dailyMealPlan));

            if (d == null)
            {
                throw new DomainException(ErrorCodes.InvalidPortion, $"Daily meal plan was not found.");
            }
            _dailyMealPlans.Remove(dailyMealPlan);
        }
    }
}
