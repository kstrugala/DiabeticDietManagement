using DiabeticDietManagement.Core.Helpers;
using DiabeticDietManagement.Core.Queries;
using DiabeticDietManagement.Infrastructure.Commands.Doctors;
using DiabeticDietManagement.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiabeticDietManagement.Infrastructure.Services
{
    public interface IDoctorService : IService
    {
        Task<DoctorDto> GetAsync(Guid Id);
        Task<PagedResult<DoctorDto>> BrowseAsync(DoctorQuery query);
        Task CreateAsync(CreateDoctor doctor);
        Task UpdateAsync(UpdateDoctor doctor);
        
    }
}
