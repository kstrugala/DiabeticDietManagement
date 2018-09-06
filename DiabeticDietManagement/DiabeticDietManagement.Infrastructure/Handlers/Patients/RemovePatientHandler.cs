using DiabeticDietManagement.Infrastructure.Commands;
using DiabeticDietManagement.Infrastructure.Commands.Patients;
using DiabeticDietManagement.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiabeticDietManagement.Infrastructure.Handlers.Patients
{
    public class RemovePatientHandler : ICommandHandler<RemovePatient>
    {
        private readonly IHandler _handler;
        private readonly IPatientService _patientService;

        public RemovePatientHandler(IHandler handler, IPatientService patientService)
        {
            _handler = handler;
            _patientService = patientService;
        }

        public async Task HandleAsync(RemovePatient command)
            => await _handler
                        .Run(async () => await _patientService.RemoveAsync(command.Id))
                        .Next()
                        .ExecuteAllAsync();
    }
}
