using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiabeticDietManagement.Core.Queries;
using DiabeticDietManagement.Infrastructure.Commands;
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

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] DoctorQuery query)
        {
            var doctors = await _doctorService.BrowseAsync(query);

            return Json(doctors);
        }
    }
}