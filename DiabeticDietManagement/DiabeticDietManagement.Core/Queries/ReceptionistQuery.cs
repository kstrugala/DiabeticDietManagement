using System;
using System.Collections.Generic;
using System.Text;

namespace DiabeticDietManagement.Core.Queries
{
    public class ReceptionistQuery : PageQuery
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
    }
}
