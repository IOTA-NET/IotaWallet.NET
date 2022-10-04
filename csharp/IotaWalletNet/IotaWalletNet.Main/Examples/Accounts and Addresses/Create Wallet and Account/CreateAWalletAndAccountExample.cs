using IotaWalletNet.Application.Common.Extensions;
using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Application.Common.Options;
using IotaWalletNet.Application.WalletContext.Commands.CreateAccount;
using IotaWalletNet.Application.WalletContext.Commands.StoreMnemonic;
using IotaWalletNet.Application.WalletContext.Queries.GetNewMnemonic;
using IotaWalletNet.Domain.Common.Models.Coin;
using IotaWalletNet.Main.Common.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace IotaWalletNet.Main.Examples.Accounts_and_Addresses
{
    public static class CreateAWalletAndAccountExample
    {

        public async static Task Run()
        {
            //Register all of the dependencies into a collection of services
            IServiceCollection services = new ServiceCollection().AddIotaWalletServices();

            //Install services to service provider which is used for dependency injection
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            //Use serviceprovider to create a scope, which disposes of all services at end of scope
            using (IServiceScope scope = serviceProvider.CreateScope())
            {
                //Request IWallet service from service provider
                IWallet wallet = scope.ServiceProvider.GetRequiredService<IWallet>();

                //Build wallet using a fluent-style configuration api
                wallet = wallet
                            .ConfigureWalletOptions()
                                .SetCoinType(TypeOfCoin.Shimmer)
                                .SetStoragePath("./walletdb")
                                .ThenBuild()
                            .ConfigureClientOptions()
                                .AddNodeUrl("https://api.testnet.shimmer.network")
                                .IsFallbackToLocalPow()
                                .IsLocalPow()
                                .ThenBuild()
                            .ConfigureSecretManagerOptions()
                                .SetPassword("password")
                                .SetSnapshotPath("./mystronghold")
                                .ThenBuild()
                            .ThenInitialize();


                //Let's generate a new Mnemonic
                GetNewMnemonicResponse getNewMnemonicResponse = await wallet.GetNewMnemonicAsync();
                string newMnemonic = getNewMnemonicResponse.Payload!;
                Console.WriteLine($"GetNewMnemonicAsync: {newMnemonic}");

                //Store into stronghold
                StoreMnemonicResponse storeMnemonicResponse = await wallet.StoreMnemonicAsync(newMnemonic);
                Console.WriteLine($"StoreMnemonicAsync: {JsonConvert.SerializeObject(storeMnemonicResponse)}");

                CreateAccountResponse response;
                //Let's create 2 accounts, with usernames cookiemonster and elmo
                (response, IAccount? cookieMonsterAccount) = await wallet.CreateAccountAsync("cookiemonster");
                Console.WriteLine($"CreateAccountAsync: {JsonConvert.SerializeObject(response)}");

                (response, IAccount? elmoAccount) = await wallet.CreateAccountAsync("elmo");
                Console.WriteLine($"CreateAccountAsync: {JsonConvert.SerializeObject(response)}");

            }
        }

    }
}
