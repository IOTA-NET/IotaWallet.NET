using IotaWalletNet.Application.AccountContext.Commands.MintNfts;
using IotaWalletNet.Application.AccountContext.Commands.SendAmount;
using IotaWalletNet.Application.AccountContext.Commands.SyncAccount;
using IotaWalletNet.Application.AccountContext.Queries.GetBalance;
using IotaWalletNet.Application.Common.Extensions;
using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Domain.Common.Extensions;
using IotaWalletNet.Domain.Common.Interfaces;
using IotaWalletNet.Domain.Common.Models.Coin;
using IotaWalletNet.Domain.Common.Models.Nft;
using IotaWalletNet.Domain.Common.Models.Output;
using IotaWalletNet.Domain.Common.Models.Transaction;
using Microsoft.Extensions.DependencyInjection;
using MimeMapping;
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
            using IServiceScope serviceScope = serviceProvider.CreateScope();

            //Request IWallet service from service provider
            IWallet wallet = serviceScope.ServiceProvider.GetRequiredService<IWallet>();

            //Build wallet using a fluent-style configuration api
            wallet = wallet
                        .ConfigureClientOptions()
                            .AddNodeUrl("https://api.testnet.shimmer.network")
                            .SetFaucetUrl("https://faucet.testnet.shimmer.network")
                            .IsFallbackToLocalPow()
                            .Then()
                        .ConfigureSecretManagerOptions()
                            .SetSnapshotPath("./mystronghold")
                            .SetPassword("password")
                            .Then()
                        .ConfigureWalletOptions()
                            .SetStoragePath("./walletdb")
                            .SetCoinType(TypeOfCoin.Shimmer)
                            .Then()
                        .Initialize();
            
            //Subscrive to events, particularly transaction inclusion
            //For simplicity, we included all events
            wallet.SubscribeToEvents(WalletEventTypes.AllEvents);

            //Let's retrieve our cookiemonster account
            (_, IAccount? account) = await wallet.GetAccountAsync("cookiemonster");

            //Let's enable periodic syncing every 5 esconds
            //We can cancel the periodic syncing with the tokenSource
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            account.EnablePeriodicSyncing(intervalInMilliSeconds: 5000, tokenSource.Token);

            //Let's create a simple random nft that just contains textual data
            NftIrc27 metadata = new NftIrc27(KnownMimeTypes.Text, "Random NFT", "www.random.com");
            List<NftOptions> nftOptions = new List<NftOptions>()
            {
                new NftOptions(){ ImmutableMetadata = JsonConvert.SerializeObject(metadata).ToHexString() }
            };
            MintNftsResponse mintNftsResponse = await account.MintNftsAsync(nftOptions);
            
            //Now lets obtain the transaction object and wait for its confirmation as well as the new outputs to be
            //added to our wallet.
            //The below could only be achieved if you have subscribed to the transaction inclusion event.
            //You also have to sync in order to receive updates. Thus, we have enabled periodic syncing to help us.
            Transaction transaction = mintNftsResponse.Payload!;
            string transactionId = transaction.TransactionId;
            Task waitConfirmationTask = transaction.WaitForConfirmationAsync(account);
            Task waitNewOutputTask = transaction.WaitForNewOutputAsync(account);
            await Task.WhenAll(waitConfirmationTask, waitNewOutputTask);
            
            //Success,the transaction have been confirmed and new outputs have arrived to our wallet!
            Console.WriteLine("Finished waiting");

            //Let's stop our periodic syncing
            tokenSource.Cancel();

            await account.SyncAccountAsync();

            List<OutputData> outputs = (await account.GetUnspentOutputsAsync()).Payload!;
            var newOutputs = outputs.Where(x => x.Metadata.TransactionId == transactionId && x.Output.Type == 6);
            Console.WriteLine(newOutputs.Count());
            await Task.Delay(200 * 1000);
        }

    }
}
