using DiabeticDietManagement.Core.Domain;
using DiabeticDietManagement.Core.Helpers;
using DiabeticDietManagement.Core.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiabeticDietManagement.Core.Repositories
{
    public interface IReceptionistRepository : IRepository
    {
        Task<Receptionist> GetAsync(Guid Id);
        Task<IEnumerable<Receptionist>> GetAllAsync();
        Task<PagedResult<Receptionist>> GetReceptionistsAsync(ReceptionistQuery query);
        Task AddAsync(Receptionist receptionist);
        Task UpdateAsync(Receptionist receptionist);
    }
}
