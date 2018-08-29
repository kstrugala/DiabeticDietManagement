using DiabeticDietManagement.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiabeticDietManagement.Core.Repositories
{
    public interface IReceptionistRepository : IRepository
    {
        Task<Receptionist> GetAsync(Guid Id);
        Task<Receptionist> GetAsync(string firstName, string lastName);
        Task<IEnumerable<Receptionist>> GetAllAsync();
        Task AddAsync(Receptionist receptionist);
        Task UpdateAsync(Receptionist receptionist);
    }
}
