using IotaWalletNet.Application.Common.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace IotaWalletNet.Tests.Common.Interfaces
{
    public class DependencyTestBase : IDisposable
    {
        protected IServiceScope _serviceScope;

        public DependencyTestBase()
        {
            //Register all of the dependencies into a collection of services
            IServiceCollection services = new ServiceCollection().AddIotaWalletServices();

            //Install services to service provider which is used for dependency injection
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            //Use serviceprovider to create a scope, which disposes of all services at end of scope
            _serviceScope = serviceProvider.CreateScope();
        }

        public void Dispose()
        {
            _serviceScope.Dispose();
        }
    }
}
