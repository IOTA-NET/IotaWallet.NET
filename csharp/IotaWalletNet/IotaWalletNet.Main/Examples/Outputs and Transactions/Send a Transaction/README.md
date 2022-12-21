# Send a Transaction

## Code Example

The following example will:

1. Initialize your wallet
2. Retrieve an account
3. Prepare SendAmountCommandMessageData
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

                //Let's send 1 shimmer, which is 1,000,000 Glow, followed by 2 shimmer, which is 2000000 glow, via a single transaction
                //The below creates 2 outputs to the receiver address and 1 more output for your balance.

                string receiverAddress = "rms1qp8rknypruss89dkqnnuedm87y7xmnmdj2tk3rrpcy3sw3ev52q0vzl42tr";

                SendAmountResponse sendAmountResponse = await account.SendAmountUsingBuilder()
                                                                        .AddAddressAndAmount(receiverAddress, 1000000)
                                                                        .AddAddressAndAmount(receiverAddress, 2000000)
                                                                        .SetTaggedDataPayload(tag: "iotawallet.net", data: "hello world")
                                                                        .SendAmountAsync();


                Console.WriteLine($"SendAmountAsync: {sendAmountResponse}");
            }
        }

        
    }
```
