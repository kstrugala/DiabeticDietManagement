using DiabeticDietManagement.Infrastructure.Commands;
using DiabeticDietManagement.Infrastructure.Commands.Users;
using DiabeticDietManagement.Infrastructure.Extensions;
using DiabeticDietManagement.Infrastructure.Services;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiabeticDietManagement.Infrastructure.Handlers.Users
{
    public class LoginHandler : ICommandHandler<Login>
    {
        private readonly IUserService _userService;
        private readonly IJwtHandler _jwtHandler;
        private readonly IMemoryCache _cache;


        public LoginHandler(IUserService userService, IJwtHandler jwtHandler, IMemoryCache cache)
        {
            _userService = userService;
            _jwtHandler = jwtHandler;
            _cache = cache;
        }

        public async Task HandleAsync(Login command)
        {
            await _userService.LoginAsync(command.Email, command.Password);

            var user = await _userService.GetAsync(command.Email);
            var jwt = _jwtHandler.CreateToken(command.Email, user.Role);

            _cache.SetJwt(command.TokenId, jwt);
        }
    }
}
