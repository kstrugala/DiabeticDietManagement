using System;
using System.Collections.Generic;
using System.Text;

namespace DiabeticDietManagement.Infrastructure.Commands.RecommendedMealPlan.Helpers
{
    public class PortionForSave
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public uint GlycemicIndex { get; set; }
        public uint GlycemicLoad { get; set; }
        public uint ServeSize { get; set; }
        public uint Carbohydrates { get; set; }
        public uint Quantity { get; set; }
    }
}
