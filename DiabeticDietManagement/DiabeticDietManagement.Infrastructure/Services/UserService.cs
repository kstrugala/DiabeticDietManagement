using AutoMapper;
using DiabeticDietManagement.Core.Domain;
using DiabeticDietManagement.Core.Repositories;
using DiabeticDietManagement.Infrastructure.DTO;
using DiabeticDietManagement.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiabeticDietManagement.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncrypter _encrypter;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IEncrypter encrypter, IMapper mapper)
        {
            _userRepository = userRepository;
            _encrypter = encrypter;
            _mapper = mapper;
        }


        public async Task<UserDto> GetAsync(Guid id)
        {
            var user = await _userRepository.GetAsync(id);

            return _mapper.Map<User, UserDto>(user);
        }

        public async Task<UserDto> GetAsync(string email)
        {
            var user = await _userRepository.GetAsync(email);

            return _mapper.Map<User, UserDto>(user);
        }

        public async Task<IEnumerable<UserDto>> BrowseAsync()
        {
            var users = await _userRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(users);
        }

        public async Task LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetAsync(email);
            if (user == null)
            {
                throw new ServiceException(ErrorCodes.InvalidCredentials, "Invalid credentials");
            }

            var hash = _encrypter.GetHash(password, user.Salt);
            if (user.Password == hash)
            {
                return;
            }
            throw new ServiceException(ErrorCodes.InvalidCredentials, "Invalid credentials");
        }

        public async Task RegisterAsync(Guid userId, string email,
            string username, string password, string role)
        {
            var user = await _userRepository.GetAsync(email);
            if (user != null)
            {
                throw new ServiceException(ErrorCodes.EmailInUse, $"User with email: '{email}' already exists.");
            }

            var salt = _encrypter.GetSalt(password);
            var hash = _encrypter.GetHash(password, salt);
            user = new User(userId, email, username, role, hash, salt);
            await _userRepository.AddAsync(user);
        }

        public async Task ChangePassword(string email, string oldPassword, string newPassword)
        {
            var user = await _userRepository.GetAsync(email);
            if (user == null)
            {
                throw new ServiceException(ErrorCodes.InvalidCredentials, "Invalid credentials");
            }

            var hash = _encrypter.GetHash(oldPassword, user.Salt);
            if (user.Password == hash)
            {
                await SetPassword(email, newPassword);
            }
            throw new ServiceException(ErrorCodes.InvalidCredentials, "Invalid credentials");
        }

        public async Task ChangeEmail(string oldEmail, string newEmail)
        {
            var user = await _userRepository.GetAsync(oldEmail);

            if (user == null)
            {
                throw new ServiceException(ErrorCodes.InvalidEmail, $"User with email: '{oldEmail}' doesn't exists.");
            }

            if (oldEmail.Equals(newEmail))
                return;

            // Check new email
            var emailCheck = await _userRepository.GetAsync(newEmail);

            if (emailCheck != null)
            {
                throw new ServiceException(ErrorCodes.EmailInUse, $"User with email: '{newEmail}' already exists.");
            }

            // Set new email
            user.SetEmail(newEmail);
            
            await _userRepository.UpdateAsync(user);

        }

        public async Task SetPassword(string email, string password)
        {
            var user = await _userRepository.GetAsync(email);
            if (user == null)
            {
                throw new ServiceException(ErrorCodes.InvalidEmail, $"User with emial:{email} doesn't exist.");
            }

            var salt = _encrypter.GetSalt(password);
            var hash = _encrypter.GetHash(password, salt);

            user.SetPassword(hash, salt);
            await _userRepository.UpdateAsync(user);
        }

        public async Task RemoveAsync(Guid id)
        {
            await _userRepository.RemoveAsync(id);
        }
    }
}
