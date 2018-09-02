using DiabeticDietManagement.Infrastructure.Commands;
using DiabeticDietManagement.Infrastructure.Commands.Doctors;
using DiabeticDietManagement.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiabeticDietManagement.Infrastructure.Handlers.Doctors
{
    public class UpdateDoctorHandler : ICommandHandler<UpdateDoctor>
    {
        private readonly IHandler _handler;
        private readonly IDoctorService _doctorService;

        public UpdateDoctorHandler(IHandler handler, IDoctorService doctorService)
        {
            _handler = handler;
            _doctorService = doctorService;
        }

        public async Task HandleAsync(UpdateDoctor command)
           => await _handler
                       .Run(async () => await _doctorService.UpdateAsync(command))
                       .Next()
                       .ExecuteAllAsync();
    }
}
