using System;
using System.Collections.Generic;
using System.Text;

namespace DiabeticDietManagement.Infrastructure.Commands.RecommendedMealPlan.Helpers
{
    public class Meal
    {
        public ISet<Portion> Products { get; set;  }
    }
}
