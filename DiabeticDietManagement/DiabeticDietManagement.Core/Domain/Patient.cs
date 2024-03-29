﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DiabeticDietManagement.Core.Domain
{
    public class Patient
    {
        public Guid UserId { get; protected set; }
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string RecommendedMealPlan { get; protected set; }
        public Guid AttendingPhysicianId { get; protected set; }

        protected Patient()
        {

        }

        public Patient(User user)
        {
            UserId = user.Id;
        }

        public Patient(User user, string firstName, string lastName)
        {
            UserId = user.Id;

            SetFirstName(firstName);
            SetLastName(lastName);
        }

        public void SetFirstName(string firstName)
        {
            if (String.IsNullOrWhiteSpace(firstName))
            {
                throw new DomainException(ErrorCodes.InvalidFirstName, "First name cannot be empty.");
            }
            FirstName = firstName;
        }

        public void SetLastName(string lastName)
        {
            if (String.IsNullOrWhiteSpace(lastName))
            {
                throw new DomainException(ErrorCodes.InvalidLastName, "Last name cannot be empty.");
            }
            LastName = lastName;
        }

        public void SetAttendigPhysician(Doctor attendingPhysician)
        {
            AttendingPhysicianId = attendingPhysician.UserId;
        }

        public void SetRecommendedMealPlan(string recommendedMealPlan)
        {
            RecommendedMealPlan = recommendedMealPlan;
        }

    }
}
