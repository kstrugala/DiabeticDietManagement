using DiabeticDietManagement.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiabeticDietManagement.Core.Repositories
{
    public interface IDoctorRepository : IRepository
    {
        Task<Doctor> GetAsync(Guid Id);
        Task<Doctor> GetAsync(string firstName, string lastName);
        Task<IEnumerable<Doctor>> GetAllAsync();
        Task AddAsync(Doctor doctor);
        Task UpdateAsync(Doctor doctor);
    }
}
