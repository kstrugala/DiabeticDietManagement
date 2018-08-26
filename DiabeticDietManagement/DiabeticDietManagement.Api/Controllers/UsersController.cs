using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiabeticDietManagement.Core.Repositories;
using DiabeticDietManagement.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiabeticDietManagement.Api.Controllers
{
    [Route("api/users")]
    public class UsersController : Controller
    {
        private IUserRepository _userRepository;
        private IUserService _userService;
        public UsersController(IUserService userService, IUserRepository userRepository)
        {
            _userService = userService;
            _userRepository = userRepository;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllAsync();
            return Json(users);
        }
    }
}