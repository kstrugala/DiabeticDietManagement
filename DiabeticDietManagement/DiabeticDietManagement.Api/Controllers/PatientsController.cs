using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiabeticDietManagement.Core.Queries;
using DiabeticDietManagement.Infrastructure.Commands;
using DiabeticDietManagement.Infrastructure.Commands.Patients;
using DiabeticDietManagement.Infrastructure.Commands.RecommendedMealPlan;
using DiabeticDietManagement.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiabeticDietManagement.Api.Controllers
{
    [Authorize(Policy = "NotPatient")]
    [Route("api/patients")]
    public class PatientsController : ApiControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly IDietaryComplianceService _dietaryComplianceService;

        public PatientsController(ICommandDispatcher commandDispatcher, IPatientService patientService, IDietaryComplianceService dietaryComplianceService) : base(commandDispatcher)
        {
            _patientService = patientService;
            _dietaryComplianceService = dietaryComplianceService;
        }

        [HttpGet("{id}", Name = "GetPatientById")]
        public async Task<IActionResult> GetPatientById(Guid id)
        {
            var patient = await _patientService.GetAsync(id);

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
                return CreatedAtRoute("GetPatientById", new { id = patient.Id }, patient);
            }

            return StatusCode(500);
        }

        [HttpPut("{id}", Name = "UpdatePatient")]
        public async Task<IActionResult> UpdatePatient(Guid id, [FromBody] UpdatePatient command)
        {
            command.Id = id;

            if (command.FirstName == null && command.LastName == null && command.Email == null)
            {
                return BadRequest();
            }

            await CommandDispatcher.DispatchAsync<UpdatePatient>(command);

            return NoContent();
        }

        [HttpDelete("{id}", Name = "RemovePatient")]
        public async Task<IActionResult> RemovePatient(Guid id)
        {
            var command = new RemovePatient();
            command.Id = id;


            await CommandDispatcher.DispatchAsync<RemovePatient>(command);

            return NoContent();
        }


        // Meal plan
        [HttpGet("{id}/recommendedmealplan")]
        public async Task<IActionResult> GetRecommendedMealPlan(Guid id, [FromQuery] bool versionForEdition = false)
        {
            if (versionForEdition)
            {
                var plan = await _patientService.GetMealPlanForEditionAsync(id);
                return Json(plan);
            }
            else
            {
                var plan = await _patientService.GetMealPlanAsync(id);
                return Json(plan);
            }
        }

        [HttpPut("{id}/recommendedmealplan")]
        public async Task<IActionResult> UpdateRecommendedMealPlan(Guid id, [FromBody] UpdateRecommendedMealPlan command)
        {
            command.Id = id;

            await CommandDispatcher.DispatchAsync<UpdateRecommendedMealPlan>(command);

            return NoContent();
        }

        // Dietary Compliance
        //[HttpGet("{id}/dietarycompliance")]
        //public async Task<IActionResult> GetDietaryCompliance(Guid id)
        //{
        //    var dc = await _dietaryComplianceService.GetPatientDietaryComplianceAsync(id);
        //    return Json(dc);
        //}

        [HttpGet("{id}/dietarycompliance")]
        public async Task<IActionResult> GetDietaryCompliance(Guid id, [FromQuery] DietaryComplianceQuery query)
        {
            var dc = await _dietaryComplianceService.GetPatientDietaryCompliancePagedAsync(id, query);
            return Json(dc);
        }

    }
}