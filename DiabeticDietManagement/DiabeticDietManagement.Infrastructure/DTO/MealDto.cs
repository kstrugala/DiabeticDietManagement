using System.Collections.Generic;

namespace DiabeticDietManagement.Infrastructure.DTO
{
    public class MealDto
    {
        public HashSet<PortionDto> Products { get; set; } = new HashSet<PortionDto>();
    }
}