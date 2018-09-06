using DiabeticDietManagement.Infrastructure.Commands;
using DiabeticDietManagement.Infrastructure.Commands.Products;
using DiabeticDietManagement.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiabeticDietManagement.Infrastructure.Handlers.Products
{
    public class UpdateProductHandler : ICommandHandler<UpdateProduct>
    {
        private readonly IHandler _handler;
        private readonly IProductService _productService;

        public UpdateProductHandler(IHandler handler, IProductService productService)
        {
            _handler = handler;
            _productService = productService;
        }

        public async Task HandleAsync(UpdateProduct command)
            => await _handler
                        .Run(async () => await _productService.UpdateAsync(command))
                        .Next()
                        .ExecuteAllAsync();
    }
}
