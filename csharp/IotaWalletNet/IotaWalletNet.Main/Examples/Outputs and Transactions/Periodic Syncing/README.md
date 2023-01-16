# Periodic Syncing

## Code Example

The following example will:

1. Initialize your wallet
2. Retrieve an account
3. Enable Periodic Syncing
4. Check current balance
5. Transfer some amount out
6. Keep checking if your balance changes without manually syncing

```cs
    public static class PeriodicSyncingExample
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

                //Let's generate an address!
                string address = (await account.GenerateAddressesAsync()).Payload!.First().Address!;

                //Do a background sync every 10 seconds = 10000 milliseconds
                await account.EnablePeriodicSyncing(10 * 1000);

                //Get Initial Balance
                string balance = (await account.GetBalanceAsync()).Payload!.BaseCoin.Total;
                string tempBalance = new string(balance);

                Console.WriteLine($"Current Balance is {balance} GLOWs.");

                //Now we request shimmer tokens into that address
                await account
                    .SendAmountUsingBuilder()
                    .AddAddressAndAmount("rms1qp8rknypruss89dkqnnuedm87y7xmnmdj2tk3rrpcy3sw3ev52q0vzl42tr", 1000000)
                    .SendAmountAsync();


                //If periodic syncing is successfull, after approximately 10-20 seconds, we are able to see our new balance
                while (balance == tempBalance)
                {

                    Console.WriteLine("Getting balance in 5 seconds...");
                    await Task.Delay(5000);
                    balance = (await account.GetBalanceAsync()).Payload!.BaseCoin.Total;
                    Console.WriteLine($"Current Balance is {balance} GLOWs.");
                }

                Console.WriteLine($"Periodic syncing worked! our new balance is {balance}");
            }
        }
    }
```
