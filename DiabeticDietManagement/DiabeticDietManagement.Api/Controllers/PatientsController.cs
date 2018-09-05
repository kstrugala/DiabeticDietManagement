using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiabeticDietManagement.Core.Queries;
using DiabeticDietManagement.Infrastructure.Commands;
using DiabeticDietManagement.Infrastructure.Commands.Patients;
using DiabeticDietManagement.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace DiabeticDietManagement.Api.Controllers
{

    [Route("api/patients")]
    public class PatientsController : ApiControllerBase
    {
        private readonly IPatientService _patientService;
        public PatientsController(ICommandDispatcher commandDispatcher, IPatientService patientService) : base(commandDispatcher)
        {
            _patientService = patientService;
        }

        [HttpGet("{email}", Name = "GetPatient")]
        public async Task<IActionResult> GetPatient(string email)
        {
            var patient = await _patientService.GetAsync(email);

            if (patient == null)
            {
                return NotFound();
            }

            return Json(patient);
        }

        [HttpGet(Name = "GetPatients")]
        public async Task<IActionResult> GetPatients([FromQuery] PatientQuery query)
        {
            var patients = await _patientService.BrowseAsync(query);
            return Json(patients);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePatient([FromBody] CreatePatient command)
        {
            if (command == null || String.IsNullOrWhiteSpace(command.Email) || String.IsNullOrWhiteSpace(command.FirstName)
              || String.IsNullOrWhiteSpace(command.LastName) || String.IsNullOrWhiteSpace(command.Password) || String.IsNullOrWhiteSpace(command.Username))
            {
                return BadRequest();
            }

            await CommandDispatcher.DispatchAsync<CreatePatient>(command);

            var patient = await _patientService.GetAsync(command.Email);

            if (patient != null)
            {
                return CreatedAtRoute("GetPatient", new { email = command.Email }, patient);
            }

            return StatusCode(500);
        }

        [HttpPut("{email}", Name = "UpdatePatient")]
        public async Task<IActionResult> UpdatePatient(string email, [FromBody] UpdatePatient command)
        {
            command.Email = email;

            if (command.FirstName == null && command.LastName == null && command.NewEmail == null)
            {
                return BadRequest();
            }

            await CommandDispatcher.DispatchAsync<UpdatePatient>(command);

            return NoContent();
        }

        [HttpDelete("{email}", Name ="RemovePatient")]
        public async Task<IActionResult> RemovePatient(string email)
        {
            var command = new RemovePatient();
            command.Email = email;

            if(String.IsNullOrWhiteSpace(email))
            {
                return BadRequest();
            }

            await CommandDispatcher.DispatchAsync<RemovePatient>(command);

            return NoContent();
        }
    }
}