using IotaWalletNet.Application.Common.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace IotaWalletNet.Tests.Common.Interfaces
{
    public class DependencyTestBase : IDisposable
    {
        protected IServiceScope _serviceScope;
        protected const String STRONGHOLD_PATH = "./stronghold";
        protected const string DATABASE_PATH = "./walletdb";

        public DependencyTestBase()
        {
            //Register all of the dependencies into a collection of services
            IServiceCollection services = new ServiceCollection().AddIotaWalletServices();

            //Install services to service provider which is used for dependency injection
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            //Use serviceprovider to create a scope, which disposes of all services at end of scope
            _serviceScope = serviceProvider.CreateScope();
        }

        public void StrongholdCleanup(string path=STRONGHOLD_PATH)
        {
            if(File.Exists(path))
                File.Delete(path);
        }

        public void DatabaseCleanup(string path=DATABASE_PATH)
        {
            if(Directory.Exists(path))
                Directory.Delete(path, true);
        }
        public void Dispose()
        {
            _serviceScope.Dispose();

            //Give enough time for services to close
            Thread.Sleep(100);

            StrongholdCleanup();
            DatabaseCleanup();

        }
    }
}
