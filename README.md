<p align="center">
    <img src="https://user-images.githubusercontent.com/12537739/192513130-58bdd96d-60c7-4303-a8b1-949c301485ef.png" width="500" >
</p>

[![Compile IotaWallet.NET](https://github.com/wireless90/IotaWallet.NET/actions/workflows/Compile.yml/badge.svg?branch=main)](https://github.com/wireless90/IotaWallet.NET/actions/workflows/Compile.yml)


# Introduction

This wallet leverages IOTA's official wallet.rs bindings and ports it over to .Net.

Now .Net developers can have a chance trying out IOTA/Shimmer as well!

# Usage Example

## Setting up your wallet and sending a command

```cs
static async Task Main(string[] args)
{
    //Collate all dependencies of the project.
    //This will help you in dependency injection
    IServiceCollection services = new ServiceCollection().AddIotaWalletServices();

    //Create your dependency injection provider
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

        string mnemonic = "sail symbol venture people general equal sight pencil slight muscle sausage faculty retreat decorate library all humor metal place mandate cake door disease dwarf";

        //Let's send a StoreMnemonic request
        string response = await wallet.StoreMnemonic(mnemonic);
        
        //Alternatively we can send a StoreMnemonic request with raw jsonified string
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

For more examples, see the [Examples](https://github.com/wireless90/IotaWallet.NET/tree/main/csharp/IotaWalletNet/IotaWalletNet.Main/Examples) directory.

# Supported Commands/Queries

## Wallet
### Commands

Commands  | Requires
------------- | -------------
StoreMnemonicAsync  | mnemonic : String
CreateAccountAsync | username : String
VerifyMnemonicAsync | mnemonic : String


### Queries

Queries | Requires
--------- | -----------
GetAccountAsync | username : String
GetAccountsAsync | -
GetNewMnemonicAsync | -

## Account

### Commands

Commands  | Requires
------------- | -------------
SyncAccountAsync     | -
SendAmountAsync | [{address:String, amount:String}]
RequestFromFaucet | {address:String, url:String}]
GenerateAddressesAsync | numberOfAddresses: int, typeOfNetwork: NetworkType 

### Queries


Queries | Requires
--------- | -----------
GetBalanceAsync | username : String
