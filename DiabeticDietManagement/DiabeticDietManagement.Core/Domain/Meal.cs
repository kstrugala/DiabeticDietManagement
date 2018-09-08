using System;
using System.Collections.Generic;
using System.Text;

namespace DiabeticDietManagement.Core.Domain
{
    public class Meal
    {
        private ISet<Portion> _portions = new HashSet<Portion>();

        public ISet<Portion> Products
        {
            get { return _portions; }
            set { _portions = new HashSet<Portion>(value); }
        }

        public Meal()
        {
            
        }

    }
}
