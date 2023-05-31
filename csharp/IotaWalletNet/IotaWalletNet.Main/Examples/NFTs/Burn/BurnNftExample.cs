using IotaWalletNet.Application.AccountContext.Commands.BurnNft;
using IotaWalletNet.Application.Common.Extensions;
using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Domain.Common.Extensions;
using IotaWalletNet.Domain.Common.Models.Coin;
using Microsoft.Extensions.DependencyInjection;
using static IotaWalletNet.Application.WalletContext.Queries.GetAccount.GetAccountQueryHandler;

namespace IotaWalletNet.Main.Examples.NFTs.Burn
{
    public static class BurnNftExample
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
                                .Then()
                            .ConfigureClientOptions()
                                .AddNodeUrl("https://api.testnet.shimmer.network")
                                .SetFaucetUrl("https://faucet.testnet.shimmer.network")
                                .IsFallbackToLocalPow()
                                .IsLocalPow()
                                .Then()
                            .ConfigureSecretManagerOptions()
                                .SetPassword("password")
                                .SetSnapshotPath("./mystronghold")
                                .Then()
                            .Initialize();


                //Let's retrieve our cookiemonster account
                (GetAccountResponse accountResponse, IAccount? account) = await wallet.GetAccountAsync("cookiemonster");
                Console.WriteLine($"GetAccountAsync: {accountResponse}");

                if (account == null)
                {
                    Console.WriteLine("There was a problem retreiving the account.");
                    return;
                }

                //Sync account
                await account.SyncAccountAsync();

                Console.WriteLine(await account.GetBalanceAsync());

                //TODO: Replace with an nft output id from your accounts.
                string outputId = "0xbb237bab9e5277867c99cb05ce89176838a57bce230a9cd593aee586bffa11b50000";

                var nftId = outputId.ComputeBlake2bHash();


                //Burn our nfts
                BurnNftResponse burnNftResponse = await account.BurnNftAsync(nftId);

                //For testnet
                Console.WriteLine($"Check your block on https://explorer.shimmer.network/testnet/block/{burnNftResponse.Payload!.BlockId}");

            }
        }
    }
}
