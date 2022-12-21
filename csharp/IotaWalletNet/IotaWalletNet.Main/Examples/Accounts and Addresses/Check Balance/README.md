# Check Balance

## Code Example

The following example will:

1. Initialize our wallet
2. Get our `cookiemonster` account
2. Sync the account
3. Retrieve the balance

```cs
    public static class CheckBalanceExample
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
                                .SetFaucetUrl("https://faucet.testnet.shimmer.network")
                                .IsFallbackToLocalPow()
                                .IsLocalPow()
                                .ThenBuild()
                            .ConfigureSecretManagerOptions()
                                .SetPassword("password")
                                .SetSnapshotPath("./mystronghold")
                                .ThenBuild()
                            .ThenInitialize();

                //We can retrieve all account info
                GetAccountsResponse getAccountsResponse = await wallet.GetAccountsAsync();

                //Or we can simply retrieve a particular account with its username
                //Let's retrieve our cookiemonster account
                (GetAccountResponse getAccountResponse, IAccount? account) = await wallet.GetAccountAsync("cookiemonster");
                Console.WriteLine($"GetAccountAsync: {getAccountResponse}");

                if (account == null)
                {
                    Console.WriteLine("There was a problem retreiving the account.");
                    return;
                }

                //Sync the account with the tangle
                await account.SyncAccountAsync();

                //Retrieve balance
                GetBalanceResponse balanceResponse = await account.GetBalanceAsync();
                Console.WriteLine($"GetBalanceAsync: {balanceResponse}");
            }
        }
    }
```
