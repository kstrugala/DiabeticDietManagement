using System;
using System.Collections.Generic;
using System.Text;

namespace DiabeticDietManagement.Infrastructure.Commands.RecommendedMealPlan.Helpers
{
    public class MealForSave
    {
        public ISet<PortionForSave> Products { get; set;  }
    }
}
