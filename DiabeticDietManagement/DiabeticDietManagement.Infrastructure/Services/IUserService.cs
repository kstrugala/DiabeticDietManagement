using DiabeticDietManagement.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiabeticDietManagement.Infrastructure.Services
{
    public interface IUserService : IService
    {
        Task<UserDto> GetAsync(string email);
        Task<IEnumerable<UserDto>> BrowseAsync();
        Task RegisterAsync(Guid userId, string email,
            string username, string password, string role);
        Task LoginAsync(string email, string password);
        Task ChangePassword(string email, string oldPassword, string newPassword);
        Task SetPassword(string email, string password);
        Task ChangeEmail(string oldEmail, string newEmail);
    }
}
