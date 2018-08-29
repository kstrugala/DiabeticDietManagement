using DiabeticDietManagement.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiabeticDietManagement.Core.Repositories
{
    public interface IPatientRepository : IRepository
    {
        Task<Patient> GetAsync(Guid Id);
        Task<Patient> GetAsync(string firstName, string lastName);
        Task<IEnumerable<Patient>> GetAllAsync();
        Task AddAsync(Patient patient);
        Task UpdateAsync(Patient patient);
        Task RemoveAsync(Guid Id);
    }
}
