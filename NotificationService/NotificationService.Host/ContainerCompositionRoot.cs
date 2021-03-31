using LightInject;
using NotificationService.Db;
using NotificationService.Repositories;
using NotificationService.Repositories.Contracts;
using NotificationService.Services;
using NotificationService.Services.Contracts;

namespace NotificationService.Host
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
                 .Register<INotificationSender, NotificationSender>(new PerContainerLifetime());

             serviceRegistry
                 .Register<ITaskRepository, TaskRepository>(new PerContainerLifetime());

             serviceRegistry
                 .Register<IUserRepository, UserRepository>(new PerContainerLifetime());
        }
    }
}