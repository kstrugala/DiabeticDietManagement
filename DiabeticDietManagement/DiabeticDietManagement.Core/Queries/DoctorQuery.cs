using System;
using System.Collections.Generic;
using System.Text;

namespace DiabeticDietManagement.Core.Queries
{
    public class DoctorQuery : PageQuery
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
    }
}
