using DiabeticDietManagement.Core.Domain.Enums;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiabeticDietManagement.Infrastructure.DTO
{
    public class DietaryComplianceDto
    {
        public DateTime Date { get; set; }
        public string MealType { get; set; }
        public bool WasComplied { get; set; }
        public IEnumerable<JObject> EatenProducts { get; set; }
    }
}
