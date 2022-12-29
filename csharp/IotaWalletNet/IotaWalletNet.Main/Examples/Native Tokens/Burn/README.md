# Burn Native Tokens

## Code Example

The following example will:

1. Initialize your account
2. Retrieve your `cookiemonster` account
3. Retrieve your balance and check for current number of remaining tokens
4. Proceed to Burn 10 tokens
5. Wait for tangle consensus and resync, following that check if the number of remaining tokens, it should decrease by 10


```cs
public static class BurnNativeTokensExample
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

                SyncAccountResponse syncAccountResponse = await account.SyncAccountAsync();
                Console.WriteLine($"SyncAccountAsync: {syncAccountResponse}");

                GetBalanceResponse getBalanceResponse = await account.GetBalanceAsync();
                Console.WriteLine($"GetBalanceAsync: {getBalanceResponse}");

                /*
                 * You can get a specific native token by looking at the tokenId.
                 * For this example, we are just getting the first native token available which has atleast 10 tokens,
                 * since we are intending to melt 10 tokens.
                 * */
                NativeTokenBalance? nativeTokenBalance = getBalanceResponse.Payload?.NativeTokens?.First(nativeTokenBalance => nativeTokenBalance.Available.FromHexEncodedAmountToInt64() > 10);

                if (nativeTokenBalance == null)
                {
                    throw new Exception("No native tokens found that has atleast 10 tokens available...");
                }

                string tokenId = nativeTokenBalance.TokenId;

                Console.WriteLine($"We currently have {nativeTokenBalance.Total.FromHexEncodedAmountToUInt64()} tokens left.");

                string amount = 10.ToHexEncodedAmount();

                BurnNativeTokensResponse burnNativeTokensResponse = await account.BurnNativeTokensAsync(tokenId, amount);

                Thread.Sleep(12000);

                await account.SyncAccountAsync();

                getBalanceResponse = await account.GetBalanceAsync();


                nativeTokenBalance = getBalanceResponse.Payload?.NativeTokens?.First(nativeTokenBalance => nativeTokenBalance.TokenId == tokenId)!;
               

                Console.WriteLine($"After burning, we now have {nativeTokenBalance.Total.FromHexEncodedAmountToUInt64()} tokens left.");
            }
        }

    }
```


