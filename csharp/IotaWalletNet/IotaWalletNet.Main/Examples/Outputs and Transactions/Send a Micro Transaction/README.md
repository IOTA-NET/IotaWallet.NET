# Send a Micro Transaction

## Code Example

The following example will:

1. Initialize your wallet
2. Retrieve an account
3. Send your micro transaction!

```cs
    public static class SendMicroTransactionExample
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

                await account.SyncAccountAsync();

                //Let's send 1 Glow, followed by 2 glow, via a single transaction
                //The below creates 2 outputs to the receiver address and 1 more output for your balance.
                //Since a micro transaction creates dust for the receiver, the sender first pays a temporary storage deposit along with the sending micro amount.
                // The receiver now bears the burden of whether to accept this transaction, as if he accepts it he needs to pay storage deposit and the sender's storage deposit
                // would be returned back to the sender.
                //Thus the receiver is given a choice of whether to acccept this transaction within the stipulated time as indicated
                //by [expirationInSeconds].
                //[1]If it expired, both the amount sent and storage deposit sent is returned back to the sender.
                //[2]If receiver rejects the transaction,  both the amount sent and storage deposit sent is returned back to the sender.
                //[3]If the receiver accepts the transaction, the sender's storage deposit is returned. In turn, the receiver now has to put in the storage deposit
                //   to claim the micro amount.
                string receiverAddress = "rms1qp8rknypruss89dkqnnuedm87y7xmnmdj2tk3rrpcy3sw3ev52q0vzl42tr";

                SendMicroAmountResponse sendMicroAmountResponse = await account.SendMicroAmountUsingBuilder()
                                                                        .AddAddressAndAmount(receiverAddress, 1, expirationInSeconds:120)
                                                                        .AddAddressAndAmount(receiverAddress, 2, expirationInSeconds:60)
                                                                        .SendMicroAmountAsync();


                Console.WriteLine($"SendMicroAmountAsync: {sendMicroAmountResponse}");

            }
        }


    }
```
