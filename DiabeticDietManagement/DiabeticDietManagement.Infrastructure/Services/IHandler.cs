using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiabeticDietManagement.Infrastructure.Services
{
    public interface IHandler : IService
    {
        IHandlerTask Run(Func<Task> runAsync);
        IHandlerTaskRunner Validate(Func<Task> validateAsync);
        Task ExecuteAllAsync();
    }
}
