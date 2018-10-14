using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DiabeticDietManagement.Core.Domain;
using DiabeticDietManagement.Core.Repositories;
using DiabeticDietManagement.Infrastructure.Commands.DietaryCompliance;
using DiabeticDietManagement.Infrastructure.DTO;
using DiabeticDietManagement.Infrastructure.Exceptions;
using Newtonsoft.Json;

namespace DiabeticDietManagement.Infrastructure.Services
{
    public class DietaryComplianceService : IDietaryComplianceService
    {
        private readonly IDietaryComplianceRepository _repository;
        private readonly IMapper _mapper;

        public DietaryComplianceService(IDietaryComplianceRepository repository, IMapper mapper, IPatientService patientService)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DietaryComplianceDto>> GetPatientDietaryComplianceAsync(Guid patientId)
        {
            var dc = await _repository.GetAsync(patientId);
            return _mapper.Map<IEnumerable<DietaryCompliance>, IEnumerable<DietaryComplianceDto>>(dc);
         
        }

        public async Task AddPatientDietaryComplianceAsync(AddDietaryCompliance command)
        {
            var dc = await _repository.GetAsync(command.PatientId, DateTime.UtcNow, command.MealType);
            if (dc == null)
            {
                await _repository.AddAsync(new DietaryCompliance(command.PatientId, command.WasComplied, command.MealType, JsonConvert.SerializeObject(command.EatenProducts)));
            }
        }

        public async Task RemovePatientDietaryComplianceAsync(Guid patientId)
        {
            await _repository.RemovePatientDietaryComplianceAsync(patientId);
        }
    }
}
