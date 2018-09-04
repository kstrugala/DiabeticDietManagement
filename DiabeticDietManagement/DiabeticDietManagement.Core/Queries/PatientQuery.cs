using System;
using System.Collections.Generic;
using System.Text;

namespace DiabeticDietManagement.Core.Queries
{
    public class PatientQuery : PageQuery
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
    }
}
