<p align="center">
    <img src="https://user-images.githubusercontent.com/12537739/193295914-2d4973cc-acea-4173-8ac3-3e49695f4281.jpg" width="300" >
</p>

[![status](https://img.shields.io/badge/Status-Alpha-yellow.svg)](https://github.com/wireless90/IotaWallet.NET)

[![Continuous Integration](https://github.com/wireless90/IotaWallet.NET/actions/workflows/Compile.yml/badge.svg?branch=main)](https://github.com/wireless90/IotaWallet.NET/actions/workflows/Compile.yml)
[![Deploy to Github NuGet](https://github.com/wireless90/IotaWallet.NET/actions/workflows/GithubNuget.yml/badge.svg)](https://github.com/wireless90/IotaWallet.NET/actions/workflows/GithubNuget.yml)
[![Deploy to NuGet.org](https://github.com/wireless90/IotaWallet.NET/actions/workflows/Nuget.yml/badge.svg)](https://github.com/wireless90/IotaWallet.NET/actions/workflows/Nuget.yml)

# Introduction

This wallet leverages IOTA's official wallet.rs bindings and ports it over to .Net.

Now .Net developers can have a chance trying out IOTA/Shimmer as well!

# Installation from Nuget.org
> Install-Package IotaWallet.Net.Domain

Or download from [here](https://www.nuget.org/packages/IotaWallet.Net.Domain/).

> Install-Package IotaWallet.Net

Or download from [here](https://www.nuget.org/packages/IotaWallet.Net/).

# Alternative Installation

You can download the nugets from the github repo itself. Look to your right under `Packages`.

# Usage Example

## Setting up your wallet and sending a command

```cs
static async Task Main(string[] args)
{
    	//Register all of the dependencies into a collection of services
	IServiceCollection services = new ServiceCollection().AddIotaWalletServices();

	//Install services to service provider which is used for dependency injection
	IServiceProvider serviceProvider = services.BuildServiceProvider();

	//Use serviceprovider to create a scope, which safely disposes of all services at end of scope
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
				.IsFallbackToLocalPow()
				.IsLocalPow()
				.ThenBuild()
			.ConfigureSecretManagerOptions()
				.SetPassword("password")
				.SetSnapshotPath("./mystronghold")
				.ThenBuild()
			.ThenInitialize();

		//Let's generate a Mnemonic
		GetNewMnemonicQueryResponse getNewMnemonicQueryResponse = await wallet.GetNewMnemonicAsync();
		string newMnemonic = getNewMnemonicQueryResponse.Payload;
		Console.WriteLine($"GetNewMnemonicAsync: {newMnemonic}");
		
		//Store into stronghold
		//Remember, Generation and storage of mnemonic only is needed to do done the first time!
		string response = await wallet.StoreMnemonicAsync(newMnemonic);
		Console.WriteLine($"StoreMnemonicAsync: {response.PrettyJson()}");

		//Let's create an accounts, with username "cookiemonster"
		(response, IAccount? account) = await wallet.CreateAccountAsync("cookiemonster");
		Console.WriteLine($"CreateAccountAsync: {response.PrettyJson()}");

		if (account == null)
		{
			Console.WriteLine("There was a problem creating the account.");
			return;
		}
		
		//Lets generate 1 new address!
		GenerateAddressesCommandResponse? generateAddressesCommandResponse = await account.GenerateAddressesAsync(numberOfAddresses: 1, NetworkType.Testnet);
		string? generatedAddress = generateAddressesCommandResponse?.Payload?.FirstOrDefault()?.Address;

		if(generatedAddress.IsNotNullAndEmpty())
			Console.WriteLine($"GenerateAddressesAsync: {generatedAddress}");
			
		//Let's request some Shimmer from the faucet
        	await account.RequestFromFaucet(generatedAddress, @"https://faucet.testnet.shimmer.network");
        
		//Let's Checkout our balance. We will sync the account, followed by checking the balance.
		//Sync the account with the tangle
		await account.SyncAccountAsync();
		//Retrieve balance
		response = await account.GetBalanceAsync();
		Console.WriteLine($"GetBalanceAsync: {response.PrettyJson()}");
		
		//Great, now that we have some test shimmer tokens to send, send to me!
		//Let's send 1 shimmer, which is 1,000,000 Glow
		(string receiverAddress, string amount) = ("rms1qz9f7vecqscfynnxacyzefwvpza0wz3r0lnnwrc8r7qhx65s5x7rx2fln5q", "1000000");
		
		//You can attach as many (address,amount) pairs as you want
		AddressesWithAmountAndTransactionOptions addressesWithAmountAndTransactionOptions = new AddressesWithAmountAndTransactionOptions();
		addressesWithAmountAndTransactionOptions
				.AddAddressAndAmount(receiverAddress, amount);

		//Start sending
		response = await account.SendAmountAsync(addressesWithAmountAndTransactionOptions);

		Console.WriteLine($"SendAmountAsync: {response.PrettyJson()}");
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
