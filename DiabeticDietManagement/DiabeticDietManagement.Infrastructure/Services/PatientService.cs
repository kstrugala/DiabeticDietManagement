using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DiabeticDietManagement.Core.Domain;
using DiabeticDietManagement.Core.Helpers;
using DiabeticDietManagement.Core.Queries;
using DiabeticDietManagement.Core.Repositories;
using DiabeticDietManagement.Infrastructure.Commands.Patients;
using DiabeticDietManagement.Infrastructure.DTO;
using DiabeticDietManagement.Infrastructure.Exceptions;

namespace DiabeticDietManagement.Infrastructure.Services
{
    public class PatientService : IPatientService
    {
        private readonly IUserService _userService;
        private readonly IPatientRepository _patientRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public PatientService(IUserService userService, IPatientRepository patientRepository, IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _userService = userService;
            _patientRepository = patientRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<PatientDto>> BrowseAsync(PatientQuery query)
        {
            var repositoryResults = await _patientRepository.GetPatientsAsync(query);
            var result = _mapper.Map<IEnumerable<Patient>, IEnumerable<PatientDto>>(repositoryResults.Results);

            foreach (var patient in result)
            {
                var user = await _userRepository.GetAsync(patient.Id);
                patient.Email = user.Email;
            }

            return new PagedResult<PatientDto>(result, repositoryResults.Pagination.TotalCount,
                                              repositoryResults.Pagination.CurrentPage, repositoryResults.Pagination.PageSize,
                                              repositoryResults.Pagination.TotalPages);
        }

        public async Task CreateAsync(CreatePatient patient)
        {
            Guid userID = Guid.NewGuid();

            if (String.IsNullOrWhiteSpace(patient.Email))
            {
                throw new ServiceException(ErrorCodes.InvalidEmail, $"Email {patient.Email} is invalid.");
            }
            if (String.IsNullOrWhiteSpace(patient.FirstName))
            {
                throw new ServiceException(ErrorCodes.InvalidFirstName, $"First {patient.FirstName} is invalid.");
            }
            if (String.IsNullOrWhiteSpace(patient.LastName))
            {
                throw new ServiceException(ErrorCodes.InvalidLastName, $"First {patient.LastName} is invalid.");
            }
            if (String.IsNullOrWhiteSpace(patient.Username))
            {
                throw new ServiceException(ErrorCodes.InvalidUsername, $"First {patient.Username} is invalid.");
            }
            if (String.IsNullOrWhiteSpace(patient.Password))
            {
                throw new ServiceException(ErrorCodes.InvalidPassword, $"Password cannot be blank.");
            }


            await _userService.RegisterAsync(userID, patient.Email, patient.Username, patient.Password, "Patient");
            var user = await _userRepository.GetAsync(userID);
            var p = new Patient(user, patient.FirstName, patient.LastName);
            await _patientRepository.AddAsync(p);
        }

        public async Task<PatientDto> GetAsync(string email)
        {
            var user = await _userService.GetAsync(email);

            if (user != null)
            {
                var patient = await _patientRepository.GetAsync(user.Id);
                if (patient != null)
                {
                    var patientDto = _mapper.Map<Patient, PatientDto>(patient);
                    return _mapper.Map<UserDto, PatientDto>(user, patientDto);
                }
            }
            return null;
        }

        public async Task RemoveAsync(string email)
        {
            var user = await _userService.GetAsync(email);

            if (user != null)
            {
                // Remove patient
                await _patientRepository.RemoveAsync(user.Id);
                // Remove user
                await _userService.RemoveAsync(user.Id);
            }
            else
                throw new ServiceException(ErrorCodes.InvalidEmail, $"User with email: {email} doesn't exist.");
        }

        public async Task UpdateAsync(UpdatePatient patient)
        {
            var user = await _userService.GetAsync(patient.Email);

            if (user == null)
            {
                throw new ServiceException(ErrorCodes.UserNotFound, $"User with email: {patient.Email} doesn't exist.");
            }

            if (user.Role.Equals("Patient"))
            {
                var pac = await _patientRepository.GetAsync(user.Id);
                if (!String.IsNullOrWhiteSpace(patient.FirstName))
                    pac.SetFirstName(patient.FirstName);
                if (!String.IsNullOrWhiteSpace(patient.LastName))
                    pac.SetLastName(patient.LastName);

                if (patient.NewEmail != null)
                {
                    await _userService.ChangeEmail(patient.Email, patient.NewEmail);
                }
                await _patientRepository.UpdateAsync(pac);
            }
            else
                throw new ServiceException(ErrorCodes.UserNotFound, $"Patient with email:{patient.Email} doesn't exist.");
        }
    }
}
