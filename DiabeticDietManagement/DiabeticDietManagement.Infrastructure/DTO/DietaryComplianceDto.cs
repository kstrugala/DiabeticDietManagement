using DiabeticDietManagement.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiabeticDietManagement.Infrastructure.DTO
{
    public class DietaryComplianceDto
    {
        public DateTime Date { get; set; }
        public MealType MealType { get; set; }
        public bool WasComplied { get; set; }
        public string EatenProducts { get; set; }
    }
}
