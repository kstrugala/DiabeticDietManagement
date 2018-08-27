using DiabeticDietManagement.Core.Domain;
using NLog;
using System;
using System.Threading.Tasks;

namespace DiabeticDietManagement.Infrastructure.Services
{
    public interface IHandlerTask
    {
        IHandlerTask Always(Func<Task> always);
        IHandlerTask OnCustomError(Func<CustomException, Task> onCustomError,
            bool propagateException = false, bool executeOnError = false);
        IHandlerTask OnCustomError(Func<CustomException, Logger, Task> onCustomError,
            bool propagateException = false, bool executeOnError = false);
        IHandlerTask OnError(Func<Exception, Task> onError, bool propagateException = false);
        IHandlerTask OnError(Func<System.Exception, Logger, Task> onError, bool propagateException = false);
        IHandlerTask OnSuccess(Func<Task> onSuccess);
        IHandlerTask PropagateException();
        IHandlerTask DoNotPropagateException();
        IHandler Next();
        Task ExecuteAsync();
    }
}