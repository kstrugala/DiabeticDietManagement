using System;
using System.Collections.Generic;
using System.Text;

namespace DiabeticDietManagement.Infrastructure.Settings
{
    public class JwtSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public long Expiry { get; set; }
    }
}
