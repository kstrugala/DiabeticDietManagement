using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DiabeticDietManagement.Infrastructure.Commands;
using DiabeticDietManagement.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiabeticDietManagement.Api.Controllers
{
    public class PatientController : ApiControllerBase
    {
        private readonly IPatientService _patientService;
        public PatientController(ICommandDispatcher commandDispatcher, IPatientService patientService) : base(commandDispatcher)
        {
            _patientService = patientService;
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
    }
}