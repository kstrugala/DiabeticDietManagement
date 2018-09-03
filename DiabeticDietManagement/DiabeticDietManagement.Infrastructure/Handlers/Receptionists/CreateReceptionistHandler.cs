using DiabeticDietManagement.Infrastructure.Commands;
using DiabeticDietManagement.Infrastructure.Commands.Receptionists;
using DiabeticDietManagement.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiabeticDietManagement.Infrastructure.Handlers.Receptionists
{
    public class CreateReceptionistHandler : ICommandHandler<CreateReceptionist>
    {
        private readonly IHandler _handler;
        private readonly IReceptionistService _receptionistService;

        public CreateReceptionistHandler(IHandler handler, IReceptionistService receptionistService)
        {
            _handler = handler;
            _receptionistService = receptionistService;
        }

        public async Task HandleAsync(CreateReceptionist command)
            => await _handler
                        .Run(async () => await _receptionistService.CreateAsync(command))
                        .Next()
                        .ExecuteAllAsync();
    }
}
