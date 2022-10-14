using IotaWalletNet.Application.Common.Extensions;
using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Domain.Common.Models.Coin;
using Microsoft.Extensions.DependencyInjection;

namespace IotaWalletNet.Tests.Common.Interfaces
{
    public class DependencyTestBase : IDisposable
    {
        protected IServiceScope _serviceScope;
        protected const String STRONGHOLD_PATH = "./stronghold";
        protected const string DATABASE_PATH = "./walletdb";
        protected const string DEFAULT_MNEMONIC = "sail symbol venture people general equal sight pencil slight muscle sausage faculty retreat decorate library all humor metal place mandate cake door disease dwarf";
        protected const string DEFAULT_API_URL = "https://api.testnet.shimmer.network";
        protected const string DEFAULT_FAUCET_URL = @"https://faucet.testnet.shimmer.network";
        protected const string ANOTHER_WALLET_ADDRESS = "rms1qz8wf6jrchvsfmcnsfhlf6s53x3u85y0j4hvwth9a5ff3xhrxtmvvyc9ae7";
        protected const int SLEEP_DURATION_SECONDS_TRANSACTION = 5;
        protected const int SLEEP_DURATION_SECONDS_FAUCET = 15;
        protected const int SLEEP_DURATION_SECONDS_API_RATE_LIMIT = 20;
        public DependencyTestBase()
        {

            StrongholdCleanup();

            DatabaseCleanup();

            //Register all of the dependencies into a collection of services
            IServiceCollection services = new ServiceCollection().AddIotaWalletServices();

            //Install services to service provider which is used for dependency injection
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            //Use serviceprovider to create a scope, which disposes of all services at end of scope
            _serviceScope = serviceProvider.CreateScope();

            RateLimitApi();
        }

        public static IWallet CreateFullWallet(IWallet wallet, string nodeUrl = DEFAULT_API_URL)
        {

            return wallet
                        .ConfigureWalletOptions()
                            .SetCoinType(TypeOfCoin.Shimmer)
                            .SetStoragePath(DATABASE_PATH)
                            .ThenBuild()
                        .ConfigureClientOptions()
                            .AddNodeUrl(nodeUrl)
                            .IsFallbackToLocalPow()
                            .IsLocalPow()
                            .ThenBuild()
                        .ConfigureSecretManagerOptions()
                            .SetPassword("password")
                            .SetSnapshotPath(STRONGHOLD_PATH)
                            .ThenBuild()
                        .ThenInitialize();
        }

        public static IWallet CreateOfflineFullWallet(IWallet wallet, string nodeUrl = DEFAULT_API_URL)
        {
            return wallet
                        .ConfigureWalletOptions()
                            .SetCoinType(TypeOfCoin.Shimmer)
                            .SetStoragePath(DATABASE_PATH)
                            .ThenBuild()
                        .ConfigureClientOptions()
                            .IsFallbackToLocalPow()
                            .IsLocalPow()
                            .ThenBuild()
                        .ConfigureSecretManagerOptions()
                            .SetPassword("password")
                            .SetSnapshotPath(STRONGHOLD_PATH)
                            .ThenBuild()
                        .ThenInitialize();
        }

        public void RateLimitApi()
        {
            Thread.Sleep(SLEEP_DURATION_SECONDS_API_RATE_LIMIT);
        }
        public void StrongholdCleanup(string path = STRONGHOLD_PATH)
        {
            if (File.Exists(path))
                File.Delete(path);
        }

        public void DatabaseCleanup(string path = DATABASE_PATH)
        {
            if (Directory.Exists(path))
                Directory.Delete(path, true);
        }
        public void Dispose()
        {
            _serviceScope.Dispose();

            //Force garbage collection
            GC.Collect();

            //Give enough time for services to close
            Thread.Sleep(100);

            StrongholdCleanup();
            DatabaseCleanup();

        }
    }
}
