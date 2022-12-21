# Subscribe to Events

## Code Example

The following example will:

1. Load our wallet
2. Subscribe to events
3. Sync and Send some amount to trigger events
4. Events that are printed to the console

```cs
	public static class EventsExample
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

                //Lets subscribe to all events

                wallet.WalletEventReceived += Wallet_WalletEventReceived;
                wallet.SubscribeToEvents(WalletEventTypes.AllEvents);

                //We can also choose the particular events we want to subscribe to
                //wallet.SubscribeToEvents(WalletEventTypes.TransactionProgress | WalletEventTypes.TransactionInclusion);

                //TODO unsubscribe from events

                //Let's retrieve our cookiemonster account
                (GetAccountResponse accountResponse, IAccount? account) = await wallet.GetAccountAsync("cookiemonster");
                Console.WriteLine($"GetAccountAsync: {accountResponse}");

                if (account == null)
                {
                    Console.WriteLine("There was a problem retreiving the account.");
                    return;
                }

                SyncAccountResponse syncAccountResponse = await account.SyncAccountAsync();
                Console.WriteLine($"SyncAccountAsync: {syncAccountResponse}");
                
                await account.ConsolidateOutputsAsync(true);

                GetBalanceResponse getBalanceResponse = await account.GetBalanceAsync();
                Console.WriteLine($"GetBalanceAsync: {getBalanceResponse}");

                //Let's send 1 shimmer, which is 1,000,000 Glow
                string receiverAddress = "rms1qz9f7vecqscfynnxacyzefwvpza0wz3r0lnnwrc8r7qhx65s5x7rx2fln5q";

                SendAmountResponse sendAmountResponse = await account.SendAmountUsingBuilder()
                                                                        .AddAddressAndAmount(receiverAddress, 1000000)
                                                                        .SendAmountAsync();

                Console.WriteLine($"SendAmountAsync: {sendAmountResponse}");


                Console.ReadLine();

            }

        }

        private static void Wallet_WalletEventReceived(object? sender, IWalletEvent? walletEvent)
        {
            Console.WriteLine($"Event Received: {JsonConvert.SerializeObject(walletEvent)}");
        }
    }
```
