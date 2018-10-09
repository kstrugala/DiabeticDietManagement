using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiabeticDietManagement.Core.Queries;
using DiabeticDietManagement.Infrastructure.Commands;
using DiabeticDietManagement.Infrastructure.Commands.Receptionists;
using DiabeticDietManagement.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiabeticDietManagement.Api.Controllers
{
    [Authorize(Policy = "admin")]
    [Route("api/receptionists")]
    public class ReceptionistsController : ApiControllerBase
    {
        private readonly IReceptionistService _receptionistService;

        public ReceptionistsController(ICommandDispatcher commandDispatcher, IReceptionistService receptionistService) : base(commandDispatcher)
        {
            _receptionistService = receptionistService;
        }

        [HttpGet("{id}", Name = "GetReceptionist")]
        public async Task<IActionResult> GetReceptionist(Guid id)
        {
            var receptionist = await _receptionistService.GetAsync(id);

            if (receptionist == null)
            {
                return NotFound();
            }

            return Json(receptionist);
        }

        [HttpGet(Name = "GetReceptionists")]
        public async Task<IActionResult> GetReceptionists([FromQuery] ReceptionistQuery query)
        {
            var doctors = await _receptionistService.BrowseAsync(query);

            return Json(doctors);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReceptionist([FromBody] CreateReceptionist command)
        {
            if (command == null || String.IsNullOrWhiteSpace(command.Email) || String.IsNullOrWhiteSpace(command.FirstName)
              || String.IsNullOrWhiteSpace(command.LastName) || String.IsNullOrWhiteSpace(command.Password) || String.IsNullOrWhiteSpace(command.Username))
            {
                return BadRequest();
            }

            await CommandDispatcher.DispatchAsync<CreateReceptionist>(command);

            var receptionist = await _receptionistService.GetAsync(command.Email);

            if (receptionist != null)
            {
                return CreatedAtRoute("GetReceptionist", new { email = command.Email }, receptionist);
            }

            return StatusCode(500);
        }

        [HttpPut("{id}", Name = "UpdateReceptionist")]
        public async Task<IActionResult> UpdateReceptionist(Guid id, [FromBody] UpdateReceptionist command)
        {
            var r = await _receptionistService.GetAsync(id);
            command.Email = r.Email;

            if (command.FirstName == null && command.LastName == null && command.NewEmail == null)
            {
                return BadRequest();
            }

            await CommandDispatcher.DispatchAsync<UpdateReceptionist>(command);

            return NoContent();
        }
    }
}