using DiabeticDietManagement.Infrastructure.Commands;
using DiabeticDietManagement.Infrastructure.Commands.DietaryCompliance;
using DiabeticDietManagement.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiabeticDietManagement.Infrastructure.Handlers.DietaryCompliance
{
    public class AddDietaryComplianceHandler : ICommandHandler<AddDietaryCompliance>
    {
        private IDietaryComplianceService _service;

        public AddDietaryComplianceHandler(IDietaryComplianceService service)
        {
            _service = service;
        }

        public async Task HandleAsync(AddDietaryCompliance command)
        {
            await _service.AddPatientDietaryComplianceAsync(command);
        }
    }
}
