using Autofac;
using DiabeticDietManagement.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DiabeticDietManagement.Infrastructure.IoC.Modules
{
    public class CommandModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(CommandModule)
                            .GetTypeInfo()
                            .Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(ICommandHandler<>))
                .InstancePerLifetimeScope();


            builder.RegisterType<CommandDispatcher>().As<ICommandDispatcher>().InstancePerLifetimeScope();
        }
    }
}
