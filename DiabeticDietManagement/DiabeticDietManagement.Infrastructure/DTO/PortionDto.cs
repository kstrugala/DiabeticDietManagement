using System;

namespace DiabeticDietManagement.Infrastructure.DTO
{
    public class PortionDto
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public uint GlycemicIndex { get; set; }
        public uint GlycemicLoad { get; set; }
        public uint ServeSize { get; set; }
        public uint Carbohydrates { get; set; }
        public uint Quantity { get; set; }
    }
}