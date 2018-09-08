using System;
using System.Collections.Generic;
using System.Text;

namespace DiabeticDietManagement.Core.Domain
{
    public class Portion
    {
        public Guid ProductId { get; set; }
        public uint Quantity { get; set; }
    }
}
