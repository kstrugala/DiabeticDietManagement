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
        private readonly ILogger _logger;

        public DataInitializer(IUserService userService, ILogger<DataInitializer> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            var users = await _userService.BrowseAsync();
            if (users.Any())
            {
                _logger.LogTrace("Users was already initialized.");
                return;
            }
            _logger.LogTrace("Initializing users...");

            await _userService.RegisterAsync(Guid.NewGuid(), "admin@test.com", "admin", "secret", "admin");

        }
    }
}
