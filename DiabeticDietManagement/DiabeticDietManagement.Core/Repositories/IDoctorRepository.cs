using DiabeticDietManagement.Core.Domain;
using DiabeticDietManagement.Core.Helpers;
using DiabeticDietManagement.Core.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiabeticDietManagement.Core.Repositories
{
    public interface IDoctorRepository : IRepository
    {
        Task<Doctor> GetAsync(Guid Id);
        Task<IEnumerable<Doctor>> GetAllAsync();
        Task<PagedResult<Doctor>> GetDoctorsAsync(DoctorQuery query);
        Task AddAsync(Doctor doctor);
        Task UpdateAsync(Doctor doctor);
    }
}
