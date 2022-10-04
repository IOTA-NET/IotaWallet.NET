using IotaWalletNet.Application.Common.Extensions;
using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Application.Common.Options;
using IotaWalletNet.Domain.Common.Models.Coin;
using IotaWalletNet.Main.Common.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace IotaWalletNet.Main.Examples.Outputs_and_Transactions.Request_Tokens_from_Faucet
{
    public static class RequestTokensFromFaucetExample
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


                //Let's retrieve our cookiemonster account
                (string response, IAccount? account) = await wallet.GetAccountAsync("cookiemonster");
                Console.WriteLine($"GetAccountAsync: {response.PrettyJson()}");

                if (account == null)
                {
                    Console.WriteLine("There was a problem retreiving the account.");
                    return;
                }

                await account.RequestFromFaucetAsync("rms1qz8wf6jrchvsfmcnsfhlf6s53x3u85y0j4hvwth9a5ff3xhrxtmvvyc9ae7", @"https://faucet.testnet.shimmer.network");
            }
        }


    }
}
