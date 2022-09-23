# IotaWallet.NET

## Usage Example

### Setting up your wallet and sending a command

```cs
static async Task Main(string[] args)
{
    //Collate all dependencies of the project.
    //This will help you in dependency injection
    IServiceCollection services = new ServiceCollection()
                                        .AddDomainServices()
                                        .AddApplicationServices()
                                        .AddConsoleServices();

    //Create your dependency injection provider
    IServiceProvider serviceProvider = services.BuildServiceProvider();

    using (IServiceScope scope = serviceProvider.CreateScope())
    {
        IWallet wallet = scope.ServiceProvider.GetRequiredService<IWallet>();

        //Configure your wallet with a fluent-styled interface
        wallet = wallet
                    .ConfigureWalletOptions()
                        .SetCoinType(WalletOptions.TypeOfCoin.Shimmer)
                        .SetStoragePath("./walletdb")
                        .ThenBuild()
                    .ConfigureClientOptions()
                        .AddNodeUrl("https://api.testnet.shimmer.network")
                        .IsOffline(false)
                        .IsFallbackToLocalPow()
                        .IsLocalPow(false)
                        .ThenBuild()
                    .ConfigureSecretManagerOptions()
                        .SetPassword("password")
                        .SetSnapshotPath("./mystronghold")
                        .ThenBuild();

        //Connect your wallet
        wallet.Connect();

        //We will be using mediator to send our commands to the rust interface
        IMediator mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        string mnemonic = "sail symbol venture people general equal sight pencil slight muscle sausage faculty retreat decorate library all humor metal place mandate cake door disease dwarf";

        //Let's send a StoreMnemonicCommand
        await mediator.Send(new StoreMnemonicCommand(wallet, mnemonic));

        //Alternatively we can send a StoreMnemonicCommand with raw jsonified string
        //wallet.SendMessage(@"
        //{
        //    ""cmd"": ""StoreMnemonic"",
        //    ""payload"": mnemonic
        //}
        //");

        /*  NOTE:
            That's about it!
            When you send a command/query, you are most likely going to get a response back from the rust library.
            Remember to subscribe to Response Notifications as seen in the next example to receive these responses.
        */

        Console.Read();

        
    }
}
```

### Subscribing to the responses

We have to inherit from `INotificationHandler<MessageReceivedNotification>`.

```cs
    public class MessageReceivedNotificationHandler : INotificationHandler<MessageReceivedNotification>
    {
        public Task Handle(MessageReceivedNotification notification, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(notification.Message))
                Console.WriteLine($"Message: {PrettyJson(notification.Message)}");

            if (!string.IsNullOrEmpty(notification.Error))
                Console.WriteLine($"Rust Bridge Errors: {PrettyJson(notification.Error)}");

            return Task.CompletedTask;
        }

        private string PrettyJson(string json)
        {
            dynamic jsonObject = JsonConvert.DeserializeObject(json)!;
            return JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
        }
    }
```

## Supported Commands/Queries

### Commands

Commands  | Requires
------------- | -------------
StoreMnemonicCommand  | mnemonic : String
CreateAccountCommand | username : String
VerifyMnemonic | mnemonic : String


### Queries

Queries | Requires
--------- | -----------
GetAccounts | -
GetAccountQuery | username/index : String
GetAccountQuery | -