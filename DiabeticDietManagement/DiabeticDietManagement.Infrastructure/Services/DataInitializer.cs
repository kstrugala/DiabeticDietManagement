using DiabeticDietManagement.Core.Queries;
using DiabeticDietManagement.Infrastructure.Commands.Doctors;
using DiabeticDietManagement.Infrastructure.Commands.Patients;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabeticDietManagement.Infrastructure.Services
{
    public class DataInitializer : IDataInitializer
    {
        private readonly IUserService _userService;
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;
        private readonly ILogger _logger;

        public DataInitializer(IUserService userService, IDoctorService doctorService, IPatientService patientService, ILogger<DataInitializer> logger)
        {
            _userService = userService;
            _doctorService = doctorService;
            _patientService = patientService;
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            var users = await _userService.BrowseAsync();
            if (users.Any())
            {
                _logger.LogTrace("Users were already initialized.");
            }
            else
            {
                _logger.LogTrace("Initializing users...");
                await _userService.RegisterAsync(Guid.NewGuid(), "admin@test.com", "admin", "secret", "admin");
            }

            var doctors = await _doctorService.BrowseAsync(new DoctorQuery());

            if (doctors.Results.Any())
            {
                _logger.LogTrace("Doctors were already initialized.");
            }
            else
            {
                _logger.LogTrace("Initializing doctors...");
                await _doctorService.CreateAsync(new CreateDoctor { Email = "doctor1@test.com", FirstName = "Jan", LastName = "Kowalski", Username = "jan-kowalski", Password = "secret" });
                await _doctorService.CreateAsync(new CreateDoctor { Email = "doctor2@test.com", FirstName = "Janina", LastName = "Nowak", Username = "janina-nowak", Password = "secret" });
            }

            
        }
    }
}
