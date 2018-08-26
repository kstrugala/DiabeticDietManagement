using Autofac;
using DiabeticDietManagement.Infrastructure.IoC.Modules;
using DiabeticDietManagement.Infrastructure.Mapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiabeticDietManagement.Infrastructure.IoC
{
    public class ContainerModule : Autofac.Module
    {
        private readonly IConfiguration _configuration;

        public ContainerModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(AutoMapperConfig.Initialize())
                .SingleInstance();

            builder.RegisterModule<CommandModule>();
            builder.RegisterModule<RepositoryModule>();
            builder.RegisterModule<ServiceModule>();
            builder.RegisterModule<SqlModule>();


            builder.RegisterModule(new SettingsModule(_configuration));
        }
    }
}
