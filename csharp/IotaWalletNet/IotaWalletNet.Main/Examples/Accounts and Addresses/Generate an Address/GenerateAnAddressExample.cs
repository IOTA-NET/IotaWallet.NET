using IotaWalletNet.Application.AccountContext.Commands.GenerateAddresses;
using IotaWalletNet.Application.Common.Extensions;
using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Application.Common.Options;
using IotaWalletNet.Domain.Common.Models.Network;
using IotaWalletNet.Main.Common.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace IotaWalletNet.Main.Examples.Accounts_and_Addresses.Generate_an_Address
{
    public static class GenerateAnAddressExample
    {
        public static async Task Run()
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
                                .SetCoinType(WalletOptions.TypeOfCoin.Shimmer)
                                .SetStoragePath("./walletdb")
                                .ThenBuild()
                            .ConfigureClientOptions()
                                .AddNodeUrl("https://api.testnet.shimmer.network")
                                .IsOffline(false)
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

                //Lets generate 1 new address!
                GenerateAddressesCommandResponse? generateAddressesCommandResponse = await account.GenerateAddressesAsync(numberOfAddresses: 1, NetworkType.Testnet);
                string? generatedAddress = generateAddressesCommandResponse?.Payload?.FirstOrDefault()?.Address;

                if (generatedAddress.IsNotNullAndEmpty())
                    Console.WriteLine($"GenerateAddressesAsync: {generatedAddress}");
            }
        }
    }
}
