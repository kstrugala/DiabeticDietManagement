using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiabeticDietManagement.Core.Domain;
using DiabeticDietManagement.Core.Queries;
using DiabeticDietManagement.Infrastructure.Commands;
using DiabeticDietManagement.Infrastructure.Commands.Doctors;
using DiabeticDietManagement.Infrastructure.DTO;
using DiabeticDietManagement.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace DiabeticDietManagement.Api.Controllers
{
    [Route("api/doctors")]
    public class DoctorsController : ApiControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorsController(ICommandDispatcher commandDispatcher, IDoctorService doctorService) : base(commandDispatcher)
        {
            _doctorService = doctorService;
        }

        [HttpGet("{email}", Name = "GetDoctor")]
        public async Task<IActionResult> GetDoctor(string email)
        {
            var doctor = await _doctorService.GetAsync(email);

            if (doctor == null)
            {
                return NotFound();
            }

            return Json(doctor);
        }

        [HttpGet(Name = "GetDoctors")]
        public async Task<IActionResult> GetDoctors([FromQuery] DoctorQuery query)
        {
            var doctors = await _doctorService.BrowseAsync(query);


            return Json(doctors);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDoctor([FromBody] CreateDoctor command)
        {
            if (command == null || String.IsNullOrWhiteSpace(command.Email) || String.IsNullOrWhiteSpace(command.FirstName)
              || String.IsNullOrWhiteSpace(command.LastName) || String.IsNullOrWhiteSpace(command.Password) || String.IsNullOrWhiteSpace(command.Username))
            {
                return BadRequest();
            }

            await CommandDispatcher.DispatchAsync<CreateDoctor>(command);

            var doctor = await _doctorService.GetAsync(command.Email);

            if (doctor != null)
            {
                return CreatedAtRoute("GetDoctor", new { email = command.Email }, doctor);
            }

            return StatusCode(500);
        }

        [HttpPut("{email}", Name ="UpdateDoctor")]
        public async Task<IActionResult> UpateDoctor(string email, [FromBody] UpdateDoctor command)
        {
            command.Email = email;

            if(command.FirstName == null && command.LastName == null && command.NewEmail == null)
            {
                return BadRequest();
            }

            await CommandDispatcher.DispatchAsync<UpdateDoctor>(command);

            return NoContent();
        }

    }
}