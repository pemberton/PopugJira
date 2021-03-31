﻿using LightInject;
using TasksService.Db;
using TasksService.Repositories;
using TasksService.Repositories.Contracts;
using TasksService.Services;
using TasksService.Services.Contracts;
using TasksService.Streams;
using TasksService.Streams.Contracts;

namespace TasksService.Host
{
    public sealed class ContainerCompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry
                .Register<IDbProvider, DbProvider>(new PerContainerLifetime());
            
            serviceRegistry
                .Register<IDbProvider, DbProvider>(new PerContainerLifetime());
            
            serviceRegistry
                .Register<IUsersRepository, UsersRepository>(new PerContainerLifetime());
            
            serviceRegistry
                .Register<IPopugTaskRepository, PopugTaskRepository>(new PerContainerLifetime());
            
            serviceRegistry
                .Register<IPopugTaskAdministrationService, PopugTaskAdministrationService>(new PerContainerLifetime());

            serviceRegistry
                .Register<ITaskBusinessEventsStream, TaskBusinessEventsStream>(new PerContainerLifetime());

            // serviceRegistry
            //     .Register<IHostedService, KafkaConsumerHandler>(new PerContainerLifetime());

        }
    }
}