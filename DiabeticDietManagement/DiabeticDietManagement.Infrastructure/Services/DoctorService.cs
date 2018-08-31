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

            return new PagedResult<DoctorDto>(result, repositoryResults.TotalCount, repositoryResults.CurrentPage, repositoryResults.PageSize, repositoryResults.TotalPages);
        }

        public async Task CreateAsync(CreateDoctor doctor)
        {
            Guid userID = Guid.NewGuid();
            await _userService.RegisterAsync(userID, doctor.Email, doctor.Username, doctor.Password, "Doctor");
            var user = await _userRepository.GetAsync(userID);
            var d = new Doctor(user, doctor.FirstName, doctor.LastName);
            await _doctorRepository.AddAsync(d);
        }

        public async Task<DoctorDto> GetAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(UpdateDoctor doctor)
        {
            throw new NotImplementedException();
        }
    }
}
