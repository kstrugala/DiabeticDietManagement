using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiabeticDietManagement.Infrastructure.Services
{
    public interface IHandlerTaskRunner
    {
        IHandlerTask Run(Func<Task> runAsync);
    }
}
