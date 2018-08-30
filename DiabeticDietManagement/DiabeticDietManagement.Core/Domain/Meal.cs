using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiabeticDietManagement.Core.Domain
{
    public class Meal
    {
        public Guid Id { get; protected set; }
        private ISet<Portion> _portions = new HashSet<Portion>();

        public IEnumerable<Portion> Portions
        {
            get { return _portions; }
            set { _portions = new HashSet<Portion>(); }
        }

        public Meal()
        {
            Id = Guid.NewGuid();
        }

        public void AddPortion(Portion portion)
        {
            var p = Portions.SingleOrDefault(x => x.ProductId == portion.ProductId && x.Quantity == portion.Quantity);

            if (p!=null)
            {
                throw new DomainException(ErrorCodes.InvalidPortion, $"Portion already exists.");
            }
            _portions.Add(portion);
        }

        public void DeletePortion(Portion portion)
        {
            var p = Portions.SingleOrDefault(x => x.ProductId == portion.ProductId && x.Quantity == portion.Quantity);

            if (p == null)
            {
                throw new DomainException(ErrorCodes.InvalidPortion, $"Portion was not found.");
            }
            _portions.Remove(portion);
        }

    }
}
