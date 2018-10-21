using DiabeticDietManagement.Infrastructure.Commands;
using DiabeticDietManagement.Infrastructure.Commands.Users;
using DiabeticDietManagement.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiabeticDietManagement.Infrastructure.Handlers.Users
{
    public class ChangePasswordHandler : ICommandHandler<ChangePassword>
    {
        private readonly IUserService _userService;
        public ChangePasswordHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task HandleAsync(ChangePassword command)
        {
            await _userService.ChangePassword(command.Email, command.OldPassword, command.NewPassword);
        }
    }
}
