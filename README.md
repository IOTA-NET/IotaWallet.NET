# IotaWallet.NET

## Usage Example

### Setting up your wallet and sending a command

```cs
static async Task Main(string[] args)
{
    //Collate all dependencies of the project.
    //This will help you in dependency injection
    IServiceCollection services = new ServiceCollection()
                                        .AddIotaWalletServices()
                                        .AddMainServices();

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
                        .IsLocalPow()
                        .ThenBuild()
                    .ConfigureSecretManagerOptions()
                        .SetPassword("password")
                        .SetSnapshotPath("./mystronghold")
                        .ThenBuild();

        //Connect your wallet
        wallet.Connect();

        string mnemonic = "sail symbol venture people general equal sight pencil slight muscle sausage faculty retreat decorate library all humor metal place mandate cake door disease dwarf";

        //Let's send a StoreMnemonicCommand
        string response = await wallet.StoreMnemonic(mnemonic);
        
        //Alternatively we can send a StoreMnemonicCommand with raw jsonified string
        //await wallet.SendMessageAsync(@"
        //{
        //    ""cmd"": ""StoreMnemonic"",
        //    ""payload"": mnemonic
        //}
        //");

        Console.Read();

        
    }
}
```

## Supported Commands/Queries

### Wallet
#### Commands

Commands  | Requires
------------- | -------------
StoreMnemonicAsync  | mnemonic : String
CreateAccountAsync | username : String
VerifyMnemonicAsync | mnemonic : String


#### Queries

Queries | Requires
--------- | -----------
GetAccountAsync | username : String
GetAccountsAsync | -
GetNewMnemonicAsync | -

### Account

#### Commands

Commands  | Requires
------------- | -------------
SyncAccountAsync     | -
SendAmountAsync | [{address:String, amount:String}]
RequestFromFaucet | {address:String, url:String}]

#### Queries


Queries | Requires
--------- | -----------
GetBalanceAsync | username : String
