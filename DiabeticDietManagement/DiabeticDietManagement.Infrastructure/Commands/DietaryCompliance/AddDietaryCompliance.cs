using DiabeticDietManagement.Core.Domain.Enums;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiabeticDietManagement.Infrastructure.Commands.DietaryCompliance
{
    public class AddDietaryCompliance : ICommand
    {
        public Guid PatientId { get; set; }
        public MealType MealType { get; set; }
        public bool WasComplied { get; set; }
        public IEnumerable<JObject> EatenProducts { get; set; }
    }
}
