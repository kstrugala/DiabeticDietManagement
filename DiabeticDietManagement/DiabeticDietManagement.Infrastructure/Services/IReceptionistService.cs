using DiabeticDietManagement.Core.Helpers;
using DiabeticDietManagement.Core.Queries;
using DiabeticDietManagement.Infrastructure.Commands.Doctors;
using DiabeticDietManagement.Infrastructure.Commands.Receptionists;
using DiabeticDietManagement.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiabeticDietManagement.Infrastructure.Services
{
    public interface IReceptionistService : IService
    {
        Task<ReceptionistDto> GetAsync(Guid id);
        Task<ReceptionistDto> GetAsync(string email);
        Task<PagedResult<ReceptionistDto>> BrowseAsync(ReceptionistQuery query);
        Task CreateAsync(CreateReceptionist receptionist);
        Task UpdateAsync(UpdateReceptionist receptionist);
    }
}
