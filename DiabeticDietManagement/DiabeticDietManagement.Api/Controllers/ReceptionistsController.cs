using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiabeticDietManagement.Core.Queries;
using DiabeticDietManagement.Infrastructure.Commands;
using DiabeticDietManagement.Infrastructure.Commands.Receptionists;
using DiabeticDietManagement.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace DiabeticDietManagement.Api.Controllers
{
    [Route("api/receptionists")]
    public class ReceptionistsController : ApiControllerBase
    {
        private readonly IReceptionistService _receptionistService;

        public ReceptionistsController(ICommandDispatcher commandDispatcher, IReceptionistService receptionistService) : base(commandDispatcher)
        {
            _receptionistService = receptionistService;
        }

        [HttpGet("{email}", Name = "GetReceptionist")]
        public async Task<IActionResult> GetReceptionist(string email)
        {
            var receptionist = await _receptionistService.GetAsync(email);

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

        [HttpPut("{email}", Name = "UpdateReceptionist")]
        public async Task<IActionResult> UpdateReceptionist(string email, [FromBody] UpdateReceptionist command)
        {
            command.Email = email;

            if (command.FirstName == null && command.LastName == null && command.NewEmail == null)
            {
                return BadRequest();
            }

            await CommandDispatcher.DispatchAsync<UpdateReceptionist>(command);

            return NoContent();
        }
    }
}