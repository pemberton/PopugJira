using AccountingService.Db;
using LightInject;

namespace AccountingService
{
    public sealed class ContainerCompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry
                .Register<IDbProvider, DbProvider>(new PerContainerLifetime());
            
            serviceRegistry
                .Register<IDbProvider, DbProvider>(new PerContainerLifetime());
            
            // serviceRegistry
            //     .Register<IUsersRepository, UsersRepository>(new PerContainerLifetime());
            //
            // serviceRegistry
            //     .Register<IPopugTaskRepository, PopugTaskRepository>(new PerContainerLifetime());
            //
            // serviceRegistry
            //     .Register<IPopugTaskAdministrationService, PopugTaskAdministrationService>(new PerContainerLifetime());
        }
    }
}