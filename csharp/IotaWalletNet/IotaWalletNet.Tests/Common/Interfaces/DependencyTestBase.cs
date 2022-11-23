using IotaWalletNet.Application.Common.Extensions;
using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Domain.Common.Models.Coin;
using Microsoft.Extensions.DependencyInjection;

namespace IotaWalletNet.Tests.Common.Interfaces
{
    public class DependencyTestBase : IDisposable
    {
        protected IServiceScope _serviceScope;
        protected const string DEFAULT_API_URL = "https://api.testnet.shimmer.network";
        protected const string DEFAULT_FAUCET_URL = @"https://faucet.testnet.shimmer.network";
        protected const string ANOTHER_WALLET_ADDRESS = "rms1qz8wf6jrchvsfmcnsfhlf6s53x3u85y0j4hvwth9a5ff3xhrxtmvvyc9ae7";
        protected const int SLEEP_DURATION_SECONDS_TRANSACTION = 10;
        protected const int SLEEP_DURATION_SECONDS_FAUCET = 15;
        protected const int SLEEP_DURATION_SECONDS_API_RATE_LIMIT = 2;
        protected List<string> filesCreated;
        public DependencyTestBase()
        {
            filesCreated = new List<string>();

            //Register all of the dependencies into a collection of services
            IServiceCollection services = new ServiceCollection().AddIotaWalletServices();

            //Install services to service provider which is used for dependency injection
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            //Use serviceprovider to create a scope, which disposes of all services at end of scope
            _serviceScope = serviceProvider.CreateScope();

            RateLimitApi();
        }

        public string GetRandomFileName(int lengthLimit = 8)
        {
            string path = Path.GetRandomFileName().Replace(".", "");
            return path.Substring(0, lengthLimit);

        }

        public string GetRandomStrongholdFilename() => $"{GetRandomFileName()}.stronghold";
        public string GetRandomDatabaseFilename() => $"{GetRandomFileName()}.db";


        public async Task<IWallet> CreateFullWalletAsync(IWallet wallet, string nodeUrl = DEFAULT_API_URL, bool shouldCreateAndStoreMnemonic = true)
        {
            string strongholdFilename = GetRandomStrongholdFilename();
            string databaseFilename = GetRandomDatabaseFilename();

            filesCreated.Add(strongholdFilename);
            filesCreated.Add(databaseFilename);

            wallet
                .ConfigureWalletOptions()
                    .SetCoinType(TypeOfCoin.Shimmer)
                    .SetStoragePath(databaseFilename)
                    .ThenBuild()
                .ConfigureClientOptions()
                    .AddNodeUrl(nodeUrl)
                    .IsFallbackToLocalPow()
                    .IsLocalPow()
                    .ThenBuild()
                .ConfigureSecretManagerOptions()
                    .SetPassword("password")
                    .SetSnapshotPath(strongholdFilename)
                    .ThenBuild()
                .ThenInitialize();


            if (shouldCreateAndStoreMnemonic)
            {

                string mnemonic = (await wallet.GetNewMnemonicAsync()).Payload!;
                await wallet.StoreMnemonicAsync(mnemonic);
            }

            return wallet;
        }

        public async Task<IWallet> CreateOfflineFullWalletAsync(IWallet wallet, string nodeUrl = DEFAULT_API_URL, bool shouldCreateAndStoreMnemonic = true)
        {
            string strongholdFilename = GetRandomStrongholdFilename();
            string databaseFilename = GetRandomDatabaseFilename();

            filesCreated.Add(strongholdFilename);
            filesCreated.Add(databaseFilename);

            wallet
                .ConfigureWalletOptions()
                    .SetCoinType(TypeOfCoin.Shimmer)
                    .SetStoragePath(strongholdFilename)
                    .ThenBuild()
                .ConfigureClientOptions()
                    .IsFallbackToLocalPow()
                    .IsLocalPow()
                    .ThenBuild()
                .ConfigureSecretManagerOptions()
                    .SetPassword("password")
                    .SetSnapshotPath(databaseFilename)
                    .ThenBuild()
                .ThenInitialize();

            if (shouldCreateAndStoreMnemonic)
            {

                string mnemonic = (await wallet.GetNewMnemonicAsync()).Payload!;
                await wallet.StoreMnemonicAsync(mnemonic);
            }

            return wallet;
        }

        public void RateLimitApi()
        {
            Thread.Sleep(SLEEP_DURATION_SECONDS_API_RATE_LIMIT * 1000);
        }

        public void DeleteStrongholdAndDatabaseFiles()
        {
            foreach (string filename in filesCreated)
            {
                if (Directory.Exists(filename))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(filename);
                    foreach (FileInfo file in directoryInfo.GetFiles())
                    {
                        file.Delete();
                    }
                    foreach (DirectoryInfo dir in directoryInfo.GetDirectories())
                    {
                        dir.Delete(true);
                    }

                    Directory.Delete(filename);
                }
                else //it is just a file
                    File.Delete(filename);
            }
        }

        public void Dispose()
        {
            _serviceScope.Dispose();

            //Force garbage collection
            GC.Collect();

            //Give enough time for services to close
            Thread.Sleep(100);

            DeleteStrongholdAndDatabaseFiles();

        }
    }
}
