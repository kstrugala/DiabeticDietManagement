using DiabeticDietManagement.Infrastructure.Commands;
using DiabeticDietManagement.Infrastructure.Commands.Products;
using DiabeticDietManagement.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiabeticDietManagement.Infrastructure.Handlers.Products
{
    public class CreateProductHandler : ICommandHandler<CreateProduct>
    {
        private readonly IHandler _handler;
        private readonly IProductService _productService;

        public CreateProductHandler(IHandler handler, IProductService productService)
        {
            _handler = handler;
            _productService = productService;
        }

        public async Task HandleAsync(CreateProduct command)
            => await _handler
                        .Run(async () => await _productService.CreateAsync(command))
                        .Next()
                        .ExecuteAllAsync();
    }
}
