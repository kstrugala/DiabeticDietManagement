using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DiabeticDietManagement.Infrastructure.Commands;
using DiabeticDietManagement.Infrastructure.Commands.Users;
using DiabeticDietManagement.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiabeticDietManagement.Api.Controllers
{
    public class UserController : ApiControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService, ICommandDispatcher commandDispatcher):base(commandDispatcher)
        {
        }

        [Authorize]
        [HttpPut("api/user/changepassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePassword command)
        {
            var email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            command.Email = email;
            await CommandDispatcher.DispatchAsync<ChangePassword>(command);
            return Ok();
        }
    }
}