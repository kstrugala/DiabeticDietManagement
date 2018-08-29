using System;
using System.Collections.Generic;
using System.Text;

namespace DiabeticDietManagement.Core.Domain
{
    public class Patient
    {
        public Guid UserId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public Guid AttendingPhysicianId { get; private set; }

        public Patient(User user)
        {
            UserId = UserId;
        }

        public Patient(User user, string firstName, string lastName)
        {
            UserId = UserId;

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
    }
}
