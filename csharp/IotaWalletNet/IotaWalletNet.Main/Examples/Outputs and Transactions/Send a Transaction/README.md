# Create a Wallet and an Account

## Code Example

The following example will:

1. Initialize your wallet
2. Retrieve an account
3. Prepare AddressesWithAmountAndTransactionOptions
4. Send your transaction!

```cs
    public static class SendTransactionExample
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

                //Let's send 1 shimmer, which is 1,000,000 Glow
                (string receiverAddress, string amount) = ("rms1qz9f7vecqscfynnxacyzefwvpza0wz3r0lnnwrc8r7qhx65s5x7rx2fln5q", "1000000");
                
                //You can attach as many (address,amount) pairs as you want
                AddressesWithAmountAndTransactionOptions addressesWithAmountAndTransactionOptions = new AddressesWithAmountAndTransactionOptions();
                addressesWithAmountAndTransactionOptions
                        .AddAddressAndAmount(receiverAddress, amount);

                response = await account.SendAmountAsync(addressesWithAmountAndTransactionOptions);

                Console.WriteLine($"SendAmountAsync: {response.PrettyJson()}");
            }
        }

        
    }
```
