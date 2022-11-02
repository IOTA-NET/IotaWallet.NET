using IotaWalletNet.Application.AccountContext.Commands.MintNativeTokens;
using IotaWalletNet.Application.AccountContext.Commands.SyncAccount;
using IotaWalletNet.Application.AccountContext.Queries.GetBalance;
using IotaWalletNet.Application.Common.Extensions;
using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Domain.Common.Extensions;
using IotaWalletNet.Domain.Common.Models.Coin;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using static IotaWalletNet.Application.WalletContext.Queries.GetAccount.GetAccountQueryHandler;

namespace IotaWalletNet.Main.Examples.Native_Tokens.Mint
{
    public static class MintNativeTokensExample
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
                (GetAccountResponse accountResponse, IAccount? account) = await wallet.GetAccountAsync("cookiemonster");
                Console.WriteLine($"GetAccountAsync: {accountResponse}");

                if (account == null)
                {
                    Console.WriteLine("There was a problem retreiving the account.");
                    return;
                }

                SyncAccountResponse syncAccountResponse =  await account.SyncAccountAsync();
                Console.WriteLine($"SyncAccountAsync: {syncAccountResponse}");


                string hexEncodedCirculatingSupply = "1000000".ToHexString();
                string hexEncodedMaximumSupply = "1500000".ToHexString();

                NativeTokenIRC30 nativeTokenMetadata = new NativeTokenIRC30("iotanet", "inet", 6)
                                                        .SetDescription("Just a test coin")
                                                        .SetUrl("https://github.com/IOTA-NET/IotaWallet.NET")
                                                        .SetLogo("C:\\Users\\mrazali\\Desktop\\iotawalletnetlogo.jpg");
                
                string nativeTokenMetadataJson = JsonConvert.SerializeObject(nativeTokenMetadata);
                
                NativeTokenOptions nativeTokenOptions = new NativeTokenOptions(hexEncodedCirculatingSupply, hexEncodedMaximumSupply)
                {
                    FoundryMetadata = nativeTokenMetadataJson.ToHexString(),
                };

                MintNativeTokensResponse mintNativeTokensResponse =  await account.MintNativeTokensAsync(nativeTokenOptions);
                Console.WriteLine($"MintNativeTokensAsync: {mintNativeTokensResponse}");
                Thread.Sleep(10000);

                await account.SyncAccountAsync();

                GetBalanceResponse response = await account.GetBalanceAsync();

                Console.WriteLine(response);
            }

        }


    }
}
