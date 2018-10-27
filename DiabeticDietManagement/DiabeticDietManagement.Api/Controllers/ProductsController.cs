using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiabeticDietManagement.Core.Queries;
using DiabeticDietManagement.Infrastructure.Commands;
using DiabeticDietManagement.Infrastructure.Commands.Products;
using DiabeticDietManagement.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace DiabeticDietManagement.Api.Controllers
{
    [Route("api/products")]
    public class ProductsController : ApiControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(ICommandDispatcher commandDispatcher, IProductService productService) : base(commandDispatcher)
        {
            _productService = productService;
        }

        [HttpGet("{id}", Name = "GetProductById")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            var product = await _productService.GetAsync(id);
            return Json(product);
        }

     
        [HttpGet(Name = "GetProducts")]
        public async Task<IActionResult> GetProducts(ProductQuery query)
        {
            var products = await _productService.BrowseAsync(query);
            return Json(products);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProduct command)
        {
            command.Id = Guid.NewGuid();

            await CommandDispatcher.DispatchAsync<CreateProduct>(command);

            var product = await _productService.GetAsync(command.Id);

            if(product!=null)
            {
                return CreatedAtRoute("GetProductById", new { id = command.Id }, product);
            }

            return StatusCode(500);
            
        }

        [HttpPut("{id}", Name = "UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProduct command)
        {
            command.Id = id;
            await CommandDispatcher.DispatchAsync<UpdateProduct>(command);
            return NoContent();
        }

        [HttpPatch]
        public async Task<IActionResult> CreateProducts([FromBody] IEnumerable<CreateProduct> commands)
        {

            foreach (var command in commands)
            {
                command.Id = Guid.NewGuid();
                await CommandDispatcher.DispatchAsync<CreateProduct>(command);

            }

            return Ok();

        }
    }
}