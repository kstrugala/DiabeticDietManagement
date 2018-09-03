using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DiabeticDietManagement.Core.Domain;
using DiabeticDietManagement.Core.Helpers;
using DiabeticDietManagement.Core.Queries;
using DiabeticDietManagement.Core.Repositories;
using DiabeticDietManagement.Infrastructure.Commands.Doctors;
using DiabeticDietManagement.Infrastructure.DTO;
using DiabeticDietManagement.Infrastructure.Exceptions;

namespace DiabeticDietManagement.Infrastructure.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IUserService _userService;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public DoctorService(IUserService userService, IDoctorRepository doctorRepository, IUserRepository userRepository, IMapper mapper)
        {
            _userService = userService;
            _doctorRepository = doctorRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<DoctorDto>> BrowseAsync(DoctorQuery query)
        {
            var repositoryResults = await _doctorRepository.GetDoctorsAsync(query);
            var result = _mapper.Map<IEnumerable<Doctor>, IEnumerable<DoctorDto>>(repositoryResults.Results);

            foreach (var doctor in result)
            {
                var user = await _userRepository.GetAsync(doctor.Id);
                doctor.Email = user.Email;
            }

            return new PagedResult<DoctorDto>(result, repositoryResults.Pagination.TotalCount,
                                              repositoryResults.Pagination.CurrentPage, repositoryResults.Pagination.PageSize,
                                              repositoryResults.Pagination.TotalPages);
        }

        public async Task CreateAsync(CreateDoctor doctor)
        {
            Guid userID = Guid.NewGuid();

            if(String.IsNullOrWhiteSpace(doctor.Email))
            {
                throw new ServiceException(ErrorCodes.InvalidEmail, $"Email {doctor.Email} is invalid.");
            }
            if (String.IsNullOrWhiteSpace(doctor.FirstName))
            {
                throw new ServiceException(ErrorCodes.InvalidFirstName, $"First {doctor.FirstName} is invalid.");
            }
            if (String.IsNullOrWhiteSpace(doctor.LastName))
            {
                throw new ServiceException(ErrorCodes.InvalidLastName, $"First {doctor.LastName} is invalid.");
            }
            if (String.IsNullOrWhiteSpace(doctor.Username))
            {
                throw new ServiceException(ErrorCodes.InvalidUsername, $"First {doctor.Username} is invalid.");
            }
            if (String.IsNullOrWhiteSpace(doctor.Password))
            {
                throw new ServiceException(ErrorCodes.InvalidPassword, $"Password cannot be blank.");
            }


            await _userService.RegisterAsync(userID, doctor.Email, doctor.Username, doctor.Password, "Doctor");
            var user = await _userRepository.GetAsync(userID);
            var d = new Doctor(user, doctor.FirstName, doctor.LastName);
            await _doctorRepository.AddAsync(d);
        }


        public async Task<DoctorDto> GetAsync(string email)
        {
            var user = await _userService.GetAsync(email);

            if(user!=null)
            {
                var doctor = await _doctorRepository.GetAsync(user.Id);
                if (doctor != null)
                {
                    var doctorDto = _mapper.Map<Doctor, DoctorDto>(doctor);
                    return _mapper.Map<UserDto, DoctorDto>(user, doctorDto);
                }
            }
            return null;
        }
        
        public async Task UpdateAsync(UpdateDoctor doctor)
        {
            var user = await _userService.GetAsync(doctor.Email);

            if(user==null)
            {
                throw new ServiceException(ErrorCodes.UserNotFound, $"User with email: {doctor.Email} doesn't exist.");
            }

            if (user.Role.Equals("Doctor"))
            {
                var doc = await _doctorRepository.GetAsync(user.Id);
                if(!String.IsNullOrWhiteSpace(doctor.FirstName))
                    doc.SetFirstName(doctor.FirstName);
                if (!String.IsNullOrWhiteSpace(doctor.LastName))
                    doc.SetLastName(doctor.LastName);

                if(doctor.NewEmail!=null)
                {
                    await _userService.ChangeEmail(doctor.Email, doctor.NewEmail);
                }
                await _doctorRepository.UpdateAsync(doc);
            }
            else
                throw new ServiceException(ErrorCodes.UserNotFound, $"Doctor with email:{doctor.Email} doesn't exist.");
        }
    }
}
