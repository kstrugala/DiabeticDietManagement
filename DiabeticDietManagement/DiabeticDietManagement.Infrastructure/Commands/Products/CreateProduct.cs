using System;
using System.Collections.Generic;
using System.Text;

namespace DiabeticDietManagement.Infrastructure.Commands.Products
{
    public class CreateProduct : ICommand
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public uint GlycemicIndex { get; set; }
        public uint GlycemicLoad { get; set; }
        public uint ServeSize { get; set; }
        public uint Carbohydrates { get; set; }
    }
}
