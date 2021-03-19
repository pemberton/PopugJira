using AuthService.BO;
using AuthService.Db;
using LightInject;

namespace AuthService.Host
{
    public sealed class ContainerCompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry
                .Register<IJwtGenerator, JwtGenerator>(new PerContainerLifetime());

            }
    }
}