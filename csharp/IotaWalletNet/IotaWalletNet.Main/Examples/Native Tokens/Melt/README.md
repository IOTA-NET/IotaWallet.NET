# Melt Native Tokens

## Code Example

The following example will:

1. Initialize your account
2. Retrieve your `cookiemonster` account
3. Retrieve your balance and check for current number of melted tokens
4. Proceed to Melt 10 tokens
5. Wait for tangle consensus and resync, following that check if the number of melted tokens have increased


```cs
    public class MeltNativeTokensExample
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

                GetFoundryOutputResponse getFoundryOutputResponse = await account.GetFoundryOutputAsync(tokenId);
                SimpleTokenScheme simpleTokenScheme = (getFoundryOutputResponse.Payload!.TokenScheme as SimpleTokenScheme)!;
                ulong originalNumberOfMeltedTokens = simpleTokenScheme.MeltedTokens.FromHexEncodedAmountToUInt64();
                ulong totalMintedSupply = simpleTokenScheme.MintedTokens.FromHexEncodedAmountToUInt64();
                ulong totalMaximumSupply = simpleTokenScheme.MaximumSupply.FromHexEncodedAmountToUInt64();
                Console.WriteLine($"Melted: {originalNumberOfMeltedTokens}\nMinted: {totalMintedSupply}\nMax: {totalMaximumSupply}");


                string amount = 10.ToHexEncodedAmount();
                
                MeltNativeTokensResponse meltNativeTokensResponse = await account.MeltNativeTokensAsync(tokenId, amount);

                Thread.Sleep(12000);

                await account.SyncAccountAsync();

                getFoundryOutputResponse = await account.GetFoundryOutputAsync(tokenId);
                simpleTokenScheme = (getFoundryOutputResponse.Payload!.TokenScheme as SimpleTokenScheme)!;
                ulong latestNumberOfMeltedTokens = simpleTokenScheme.MeltedTokens.FromHexEncodedAmountToUInt64();

                if (latestNumberOfMeltedTokens > originalNumberOfMeltedTokens)
                    Console.WriteLine($"Melted {amount.FromHexEncodedAmountToInt64()} number of tokens successfully.");
                else
                    throw new Exception("Melting of tokens was unsuccessful or you might have to wait longer for your transaction to be confirmed.");
            }
        }

    }
```


