using DiabeticDietManagement.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiabeticDietManagement.Core.Domain
{
    public class DietaryCompliance
    {
        public Guid Id { get; protected set; }
        public Guid PatientId { get; protected set; }
        public DateTime Date { get; protected set; }
        public MealType MealType { get; protected set; }
        public bool WasComplied { get; protected set; }
        public string EatenProducts { get; protected set; }

        protected DietaryCompliance()
        {

        }

        public DietaryCompliance(Guid patientId, bool wasComplied, MealType mealType, string eatenProducts="")
        {
            Id = Guid.NewGuid();
            PatientId = patientId;
            Date = DateTime.UtcNow;
            MealType = mealType;
            WasComplied = wasComplied;
            EatenProducts = eatenProducts;    
        }
    }
}
