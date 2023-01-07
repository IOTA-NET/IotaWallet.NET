# Claim Outputs Example

## Code Example

The following example will:

1. Initialize your wallet
2. Retrieve an account
3. Sync account
4. Check if there are any outputs with Additional Unlock Conditions
5. Claim these outputs

```cs

  public static class ClaimOutputsExample
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

                //Sync account so that we can get the latest changes from the tangle
                await account.SyncAccountAsync();

                // Get outputs with unlock conditions
                GetOutputsWithAdditionalUnlockConditionsResponse getOutputsWithAdditionalUnlockConditionsResponse =
                    await account.GetOutputsWithAdditionalUnlockConditionsAsync(OutputTypeToClaim.All);

                //Retrieve all their outputids
                List<string> outputIds = getOutputsWithAdditionalUnlockConditionsResponse.Payload!;

                if(outputIds.Any())
                {
                    await account.ClaimOutputsAsync(outputIds);
                }

            }
        }


    }


```
