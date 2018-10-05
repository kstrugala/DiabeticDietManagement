using System;
using System.Collections.Generic;
using System.Text;

namespace DiabeticDietManagement.Infrastructure.DTO
{
    public class MealForEditionDto
    {
        public HashSet<PortionForEditionDto> Products { get; set; } = new HashSet<PortionForEditionDto>();
    }
}
