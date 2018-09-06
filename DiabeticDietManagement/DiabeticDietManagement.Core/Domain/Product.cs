using System;
using System.Collections.Generic;
using System.Text;

namespace DiabeticDietManagement.Core.Domain
{
    public class Product
    {
        public Guid Id { get; protected set; }
        public string ProductName { get; protected set; }
        public uint GlycemicIndex { get; protected set; }
        public uint GlycemicLoad { get; protected set; }
        public uint ServeSize { get; protected set; }
        public uint Carbohydrates { get; protected set; }

        protected Product()
        {

        }

        public Product(string productName, uint glycemicIndex, uint glycemicLoad, uint serveSize, uint carbohydrates)
        {
            Id = Guid.NewGuid();
            SetProductName(ProductName);
            SetGlycemicIndex(GlycemicIndex);
            SetGlycemicLoad(GlycemicLoad);
            SetServeSize(ServeSize);
            SetCarbohydrates(Carbohydrates);
        }

        public Product(Guid id, string productName, uint glycemicIndex, uint glycemicLoad, uint serveSize, uint carbohydrates)
        {
            Id = id;
            SetProductName(productName);
            SetGlycemicIndex(glycemicIndex);
            SetGlycemicLoad(glycemicLoad);
            SetServeSize(serveSize);
            SetCarbohydrates(carbohydrates);
        }

        public void SetCarbohydrates(uint carbohydrates)
        {
            Carbohydrates = carbohydrates;
        }

        public void SetServeSize(uint serveSize)
        {
            ServeSize = serveSize;
        }

        public void SetGlycemicLoad(uint glycemicLoad)
        {
            GlycemicLoad = glycemicLoad;
        }

        public void SetGlycemicIndex(uint glycemicIndex)
        {
            GlycemicIndex = glycemicIndex;
        }

        public void SetProductName(string productName)
        {
            if (String.IsNullOrWhiteSpace(productName))
                throw new DomainException(ErrorCodes.InvalidProductName, "Product name cannot be emtpy.");
            ProductName = productName;
        }
    }
}
