# Send a Data-Only Transaction

## Code Example

The following example will:

1. Initialize your wallet
2. Retrieve an account
3. Build a basic output with Metadata
4. Send the output


```cs
	public static class SendDataOnlyTransactionExample
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

                //Let's sync our account with the tangle
                await account.SyncAccountAsync();


                string receiverAddress = "rms1qp8rknypruss89dkqnnuedm87y7xmnmdj2tk3rrpcy3sw3ev52q0vzl42tr";

                //Let's build an output manually ourselves!
                //We simply add the metadata feaeture to include our data, and the address unlock condition which is the receiver's address
                BuildBasicOutputResponse buildBasicOutputResponse = await account.BuildBasicOutputUsingBuilder()
                                                                                    .Features
                                                                                        .AddMetadataFeature(data: "Hello world!")
                                                                                        .Then()
                                                                                    .UnlockConditions
                                                                                        .SetAddressUnlockConditionUsingBech32(receiverAddress)
                                                                                        .Then()
                                                                                    .BuildBasicOutputAsync();

                BasicOutput basicOutput = buildBasicOutputResponse.Payload!;

                //Let's send out the basicOutput
                SendOutputsResponse sendOutputsResponse = await account.SendOutputsAsync(new List<IOutputType>() { basicOutput });

                Console.WriteLine($"SendOutputsResponse: {sendOutputsResponse}");

            }
        }


    }
```