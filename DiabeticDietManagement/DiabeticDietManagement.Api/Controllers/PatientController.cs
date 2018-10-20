using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DiabeticDietManagement.Core.Queries;
using DiabeticDietManagement.Infrastructure.Commands;
using DiabeticDietManagement.Infrastructure.Commands.DietaryCompliance;
using DiabeticDietManagement.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiabeticDietManagement.Api.Controllers
{
    public class PatientController : ApiControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly IDietaryComplianceService _dietaryComplianceService;

        public PatientController(ICommandDispatcher commandDispatcher, IPatientService patientService, IDietaryComplianceService dietaryComplianceService) : base(commandDispatcher)
        {
            _patientService = patientService;
            _dietaryComplianceService = dietaryComplianceService;
        }

        [Authorize(Policy = "Patient")]
        [Route("api/patient/recommendedmealplan")]
        public async Task<IActionResult> GetRecomendedMealPlan()
        {
            var email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var patient = await _patientService.GetAsync(email);
            var plan = await _patientService.GetMealPlanAsync(patient.Id);

            return Json(plan);
        }

        //// Dietary Compliance
        //[Authorize(Policy = "Patient")]
        //[HttpGet("api/patient/dietarycompliance")]
        //public async Task<IActionResult> GetDietaryCompliance(Guid id)
        //{
        //    var email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //    var patient = await _patientService.GetAsync(email);

        //    var dc = await _dietaryComplianceService.GetPatientDietaryComplianceAsync(patient.Id);
        //    return Json(dc);
        //}

        // Dietary Compliance
        [Authorize(Policy = "Patient")]
        [HttpGet("api/patient/dietarycompliance")]
        public async Task<IActionResult> GetDietaryCompliance([FromQuery] DietaryComplianceQuery query)
        {
            var email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var patient = await _patientService.GetAsync(email);

            var dc = await _dietaryComplianceService.GetPatientDietaryCompliancePagedAsync(patient.Id, query);
            return Json(dc);
        }

        [Authorize(Policy = "Patient")]
        [HttpPost("api/patient/dietarycompliance")]
        public async Task<IActionResult> AddDietaryCompliance([FromBody] AddDietaryCompliance command)
        {
            var email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var patient = await _patientService.GetAsync(email);
            command.PatientId = patient.Id;
            await CommandDispatcher.DispatchAsync<AddDietaryCompliance>(command);
            return Ok();
        }
    }
}