using System;
using System.Collections.Generic;
using System.Text;

namespace DiabeticDietManagement.Infrastructure.Commands.Patients
{
    public class RemovePatient : ICommand
    {
        public Guid Id { get; set; }
    }
}
