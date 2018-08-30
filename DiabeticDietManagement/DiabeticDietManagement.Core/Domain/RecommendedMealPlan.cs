using System;
using System.Collections.Generic;
using System.Text;

namespace DiabeticDietManagement.Core.Domain
{
    public class RecommendedMealPlan
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public Guid DoctorId { get; protected set; }

        private ISet<DailyMealPlan> _dailyMealPlans = new HashSet<DailyMealPlan>();


        public IEnumerable<DailyMealPlan> DailyMealPlans
        {
            get { return _dailyMealPlans; }
            set { _dailyMealPlans = new HashSet<DailyMealPlan>(); ; }
        }

        protected RecommendedMealPlan()
        {

        }

        public RecommendedMealPlan(Guid doctorId, string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public void AddDailyMealPlan(DailyMealPlan dailyMealPlan)
        {
            var d = DailyMealPlan.SingleOrDefault(x => x.Equals(dailyMealPlan));

            if (d != null)
            {
                throw new DomainException(ErrorCodes.InvalidPortion, $"Daily meal plan already exists.");
            }
            _dailyMealPlans.Add(dailyMealPlan);
        }

        public void RemoveDailyMealPlan(DailyMealPlan dailyMealPlan)
        {
            var d = DailyMealPlan.SingleOrDefault(x => x.Equals(dailyMealPlan));

            if (d == null)
            {
                throw new DomainException(ErrorCodes.InvalidPortion, $"Daily meal plan was not found.");
            }
            _dailyMealPlans.Remove(dailyMealPlan);
        }
    }
}
