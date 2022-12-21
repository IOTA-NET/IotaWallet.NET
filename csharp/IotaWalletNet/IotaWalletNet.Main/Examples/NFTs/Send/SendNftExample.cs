using IotaWalletNet.Application.AccountContext.Commands.SendNfts;
using IotaWalletNet.Application.Common.Extensions;
using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Domain.Common.Extensions;
using IotaWalletNet.Domain.Common.Models.Address;
using IotaWalletNet.Domain.Common.Models.Coin;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using static IotaWalletNet.Application.WalletContext.Queries.GetAccount.GetAccountQueryHandler;

namespace IotaWalletNet.Main.Examples.NFTs.Send
{
    public static class SendNftExample
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
                Console.WriteLine($"GetAccountAsync: {JsonConvert.SerializeObject(accountResponse)}");

                if (account == null)
                {
                    Console.WriteLine("There was a problem retreiving the account.");
                    return;
                }

                await account.SyncAccountAsync();


                //TODO: Replace with the address of your choice!
                string receiverAddress = "rms1qz8wf6jrchvsfmcnsfhlf6s53x3u85y0j4hvwth9a5ff3xhrxtmvvyc9ae7";

                //TODO: Replace with an nft output id from your accounts.
                string outputId = "0x9c5fc8b575e29377e0401d2cd6138c0f4859fbb95b5acf0ea81b3354de6eb2e70000";

                var nftId = outputId.ComputeBlake2bHash();

                AddressAndNftId addressAndNftId = new AddressAndNftId(receiverAddress, nftId);

                SendNftsResponse sendNftsResponse = await account.SendNftsAsync(new List<AddressAndNftId> { addressAndNftId });

                //For testnet
                Console.WriteLine($"Check your transaction on https://explorer.shimmer.network/testnet/block/{sendNftsResponse.Payload!.TransactionId}");
            }
        }


    }
}
