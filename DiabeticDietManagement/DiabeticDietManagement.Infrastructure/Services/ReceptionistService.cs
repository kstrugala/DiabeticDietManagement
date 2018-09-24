using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DiabeticDietManagement.Core.Domain;
using DiabeticDietManagement.Core.Helpers;
using DiabeticDietManagement.Core.Queries;
using DiabeticDietManagement.Core.Repositories;
using DiabeticDietManagement.Infrastructure.Commands.Receptionists;
using DiabeticDietManagement.Infrastructure.DTO;
using DiabeticDietManagement.Infrastructure.Exceptions;

namespace DiabeticDietManagement.Infrastructure.Services
{
    public class ReceptionistService : IReceptionistService
    {
        private readonly IUserService _userService;
        private readonly IReceptionistRepository _receptionistRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ReceptionistService(IUserService userService, IReceptionistRepository receptionistRepository, IUserRepository userRepository, IMapper mapper)
        {
            _userService = userService;
            _receptionistRepository = receptionistRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<ReceptionistDto>> BrowseAsync(ReceptionistQuery query)
        {
            var repositoryResults = await _receptionistRepository.GetReceptionistsAsync(query);
            var result = _mapper.Map<IEnumerable<Receptionist>, IEnumerable<ReceptionistDto>>(repositoryResults.Results);

            foreach (var receptionist in result)
            {
                var user = await _userRepository.GetAsync(receptionist.Id);
                receptionist.Email = user.Email;
            }

            return new PagedResult<ReceptionistDto>(result, repositoryResults.Pagination.TotalCount,
                                              repositoryResults.Pagination.CurrentPage, repositoryResults.Pagination.PageSize,
                                              repositoryResults.Pagination.TotalPages);
        }

        public async Task CreateAsync(CreateReceptionist receptionist)
        {
            Guid userID = Guid.NewGuid();

            if (String.IsNullOrWhiteSpace(receptionist.Email))
            {
                throw new ServiceException(ErrorCodes.InvalidEmail, $"Email {receptionist.Email} is invalid.");
            }
            if (String.IsNullOrWhiteSpace(receptionist.FirstName))
            {
                throw new ServiceException(ErrorCodes.InvalidFirstName, $"First {receptionist.FirstName} is invalid.");
            }
            if (String.IsNullOrWhiteSpace(receptionist.LastName))
            {
                throw new ServiceException(ErrorCodes.InvalidLastName, $"First {receptionist.LastName} is invalid.");
            }
            if (String.IsNullOrWhiteSpace(receptionist.Username))
            {
                throw new ServiceException(ErrorCodes.InvalidUsername, $"First {receptionist.Username} is invalid.");
            }
            if (String.IsNullOrWhiteSpace(receptionist.Password))
            {
                throw new ServiceException(ErrorCodes.InvalidPassword, $"Password cannot be blank.");
            }


            await _userService.RegisterAsync(userID, receptionist.Email, receptionist.Username, receptionist.Password, "Receptionist");
            var user = await _userRepository.GetAsync(userID);
            var r = new Receptionist(user, receptionist.FirstName, receptionist.LastName);
            await _receptionistRepository.AddAsync(r);
        }

        public async Task<ReceptionistDto> GetAsync(string email)
        {
            var user = await _userService.GetAsync(email);

            if (user != null)
            {
                var receptionist = await _receptionistRepository.GetAsync(user.Id);

                if (receptionist != null)
                {
                    var receptionistDto = _mapper.Map<Receptionist, ReceptionistDto>(receptionist);
                    return _mapper.Map<UserDto, ReceptionistDto>(user, receptionistDto);
                }
            }
            return null;
        }

        public async Task<ReceptionistDto> GetAsync(Guid id)
        {
            var user = await _userService.GetAsync(id);

            if (user != null)
            {
                var receptionist = await _receptionistRepository.GetAsync(user.Id);

                if (receptionist != null)
                {
                    var receptionistDto = _mapper.Map<Receptionist, ReceptionistDto>(receptionist);
                    return _mapper.Map<UserDto, ReceptionistDto>(user, receptionistDto);
                }
            }
            return null;
        }

        public async Task UpdateAsync(UpdateReceptionist receptionist)
        {
            var user = await _userService.GetAsync(receptionist.Email);

            if (user == null)
            {
                throw new ServiceException(ErrorCodes.UserNotFound, $"User with email: {receptionist.Email} doesn't exist.");
            }

            if (user.Role.Equals("Receptionist"))
            {
                var doc = await _receptionistRepository.GetAsync(user.Id);
                if (!String.IsNullOrWhiteSpace(receptionist.FirstName))
                    doc.SetFirstName(receptionist.FirstName);
                if (!String.IsNullOrWhiteSpace(receptionist.LastName))
                    doc.SetLastName(receptionist.LastName);

                if (receptionist.NewEmail != null)
                {
                    await _userService.ChangeEmail(receptionist.Email, receptionist.NewEmail);
                }
                await _receptionistRepository.UpdateAsync(doc);
            }
            else
                throw new ServiceException(ErrorCodes.UserNotFound, $"Receptionist with email:{receptionist.Email} doesn't exist.");
        }
    }
}