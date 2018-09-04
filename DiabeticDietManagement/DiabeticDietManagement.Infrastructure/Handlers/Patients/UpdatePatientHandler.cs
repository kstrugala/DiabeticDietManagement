using DiabeticDietManagement.Infrastructure.Commands;
using DiabeticDietManagement.Infrastructure.Commands.Patients;
using DiabeticDietManagement.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiabeticDietManagement.Infrastructure.Handlers.Patients
{
    public class UpdatePatientHandler : ICommandHandler<UpdatePatient>
    {
        private readonly IHandler _handler;
        private readonly IPatientService _patientService;

        public UpdatePatientHandler(IHandler handler, IPatientService patientService)
        {
            _handler = handler;
            _patientService = patientService;
        }

        public async Task HandleAsync(UpdatePatient command)
            => await _handler
                        .Run(async () => await _patientService.UpdateAsync(command))
                        .Next()
                        .ExecuteAllAsync();
    }
}
