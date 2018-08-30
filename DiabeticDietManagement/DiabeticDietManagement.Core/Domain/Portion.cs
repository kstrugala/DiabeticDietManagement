using System;
using System.Collections.Generic;
using System.Text;

namespace DiabeticDietManagement.Core.Domain
{
    public class Portion
    {
        public Guid Id { get; protected set; }
        public Guid ProductId { get; protected set; }
        public uint Quantity { get; protected set; }

        protected Portion()
        {

        }

        public Portion(Guid productId, uint quantity)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            Quantity = quantity;
        }

        public Portion Add(Guid productId, uint quantity)
            => new Portion(productId, quantity);

    }
}
