using IotaWalletNet.Application.AccountContext.Commands.SendAmount;
using IotaWalletNet.Application.AccountContext.Commands.SyncAccount;
using IotaWalletNet.Application.AccountContext.Queries.GetBalance;
using IotaWalletNet.Application.Common.Extensions;
using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Domain.Common.Interfaces;
using IotaWalletNet.Domain.Common.Models.Coin;
using IotaWalletNet.Domain.Common.Models.Transaction;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using static IotaWalletNet.Application.WalletContext.Queries.GetAccount.GetAccountQueryHandler;
using static IotaWalletNet.Domain.Common.Models.Events.EventTypes;

namespace IotaWalletNet.Main.Examples.Events.WaitForTransactionConfirmation
{
    public static class WaitForTransactionConfirmationExample
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

                //We can subscrive to all events using WalletEventTypes.AllEvents
                //Howevever for this example, is only focussed on waiting for a transaction to complete.
                //Hence only the TransactionInclusion event is of interest.
                wallet.SubscribeToEvents(WalletEventTypes.TransactionInclusion);


                //Let's retrieve our cookiemonster account
                (GetAccountResponse accountResponse, IAccount? account) = await wallet.GetAccountAsync("cookiemonster");

                //We can also opt for periodic syncing of our account,
                //so that we don't have to worry about manual syncing
                //Below, we want to sync periodically 30 times.
                //Set count to 0 for forever periodic syncing
                account.EnablePeriodicSyncing(intervalInMilliSeconds: 3000, count: 30);

                GetBalanceResponse getBalanceResponse = await account.GetBalanceAsync();
                Console.WriteLine($"Current balance is : {getBalanceResponse.Payload!.BaseCoin.Total}");

                //Let's send 1 shimmer, which is 1,000,000 Glow
                string receiverAddress = "rms1qz9f7vecqscfynnxacyzefwvpza0wz3r0lnnwrc8r7qhx65s5x7rx2fln5q";

                SendAmountResponse sendAmountResponse = await account.SendAmountUsingBuilder()
                                                                        .AddAddressAndAmount(receiverAddress, 1000000)
                                                                        .SendAmountAsync();
                Transaction transaction = sendAmountResponse.Payload!;

                //We will setup the event handler for you and let you proceed once we receive
                //confirmation from the node that the transactionid has been confirmed.
                await transaction.WaitForConfirmationAsync(wallet);

                getBalanceResponse = await account.GetBalanceAsync();
                Console.WriteLine($"New balance is : {getBalanceResponse.Payload!.BaseCoin.Total}");


                await Task.Delay(200 * 1000);
            }

        }

    }
}
