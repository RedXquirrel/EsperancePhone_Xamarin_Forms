﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using esperancephone.Interfaces;
using esperancephone.Managers;
using esperancephone.Services;
using esperancephone.ViewModels;

namespace esperancephone.Ioc
{
    public class AppSetup
    {
        public IContainer CreateContainer()
        {
            var containerBuilder = new ContainerBuilder();
            RegisterDependencies(containerBuilder);
            return containerBuilder.Build();
        }

        protected virtual void RegisterDependencies(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterInstance(new EsperancePhoneApiManager()).As<IEsperancePhoneApiManager>();
            containerBuilder.RegisterInstance(new SettingsService()).As<ISettingsService>();
            containerBuilder.RegisterType<LoginViewModel>().SingleInstance();
        }
    }
}