using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiabeticDietManagement.Infrastructure.Services
{
    public class Handler : IHandler
    {
        private readonly ISet<IHandlerTask> _handlerTasks = new HashSet<IHandlerTask>();

        public IHandlerTask Run(Func<Task> runAsync)
        {
            var handlerTask = new HandlerTask(this, runAsync);
            _handlerTasks.Add(handlerTask);

            return handlerTask;
        }

        public IHandlerTaskRunner Validate(Func<Task> validateAsync)
            => new HandlerTaskRunner(this, validateAsync, _handlerTasks);

        public async Task ExecuteAllAsync()
        {
            foreach (var handlerTask in _handlerTasks)
            {
                await handlerTask.ExecuteAsync();
            }
            _handlerTasks.Clear();
        }
    }
}
