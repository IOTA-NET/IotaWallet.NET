<p align="center">
    <img src="https://user-images.githubusercontent.com/12537739/193295914-2d4973cc-acea-4173-8ac3-3e49695f4281.jpg" width="300" >
</p>

[![status](https://img.shields.io/badge/Status-Alpha-yellow.svg)](https://github.com/wireless90/IotaWallet.NET)

[![Continuous Integration](https://github.com/wireless90/IotaWallet.NET/actions/workflows/Compile.yml/badge.svg?branch=main)](https://github.com/IOTA-NET/IotaWallet.NET/actions/workflows/Compile.yml)
[![Deploy to Github NuGet](https://github.com/IOTA-NET/IotaWallet.NET/actions/workflows/GithubNuget.yml/badge.svg?branch=v0.2.8-alpha)](https://github.com/IOTA-NET/IotaWallet.NET/actions/workflows/GithubNuget.yml)
[![Deploy to NuGet.org](https://github.com/wireless90/IotaWallet.NET/actions/workflows/Nuget.yml/badge.svg)](https://github.com/IOTA-NET/IotaWallet.NET/actions/workflows/Nuget.yml)

# Introduction :smile:

This wallet leverages IOTA's official wallet.rs bindings and ports it over to .Net.

Now .Net developers can have a chance trying out IOTA/Shimmer as well!

# Installation from Nuget.org

### [!] Note the following two packages must be installed explicitly, do not skip it. !!! :rage:

> dotnet add package IotaWallet.Net.Domain --prerelease

Or download from [here](https://www.nuget.org/packages/IotaWallet.Net.Domain/).

> dotnet add package IotaWallet.Net --prerelease

Or download from [here](https://www.nuget.org/packages/IotaWallet.Net/).

### Architecture support :grin:

It currently supports `Windows x64` and `Linux x86_64`.

### Additional Instructions for Linux :open_mouth:

After installing `IotaWallet.Net.Domain`, when you build using `dotnet build`, you would see a file `libiota_wallet.so`. This is the precompiled rust bindings. You need to add it to your lib path.

Example...
> export LD_LIBRARY_PATH="<folder path to the library>"

Note that its the folder path, not the filepath.

# Alternative Installation :dizzy:

You can download the nugets from the github repo itself. Look to your right under `Packages`.

# Usage Example :runner:

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

		//Let's generate a Mnemonic
		GetNewMnemonicResponse getNewMnemonicResponse = await wallet.GetNewMnemonicAsync();
		Console.WriteLine($"GetNewMnemonicAsync: {getNewMnemonicResponse}");
		string newMnemonic = getNewMnemonicResponse.Payload;
		
		//Store into stronghold
		//Remember, Generation and storage of mnemonic only is needed to do done the first time!
		StoreMnemonicResponse storeMnemonicResponse = await wallet.StoreMnemonicAsync(newMnemonic);
		Console.WriteLine($"StoreMnemonicAsync: {storeMnemonicResponse}");

		//Let's create an accounts, with username "cookiemonster"
		(CreateAccountResponse createAccountResponse, IAccount? account) = await wallet.CreateAccountAsync("cookiemonster");
		Console.WriteLine($"CreateAccountAsync: {createAccountResponse}");

		if (account == null)
		{
			Console.WriteLine("There was a problem creating the account.");
			return;
		}
		
		//Lets generate 1 new address!
		GenerateAddressesResponse generateAddressesResponse = await account.GenerateAddressesAsync(numberOfAddresses: 1, NetworkType.Testnet);
		Console.WriteLine($"GenerateAddressesAsync: {generateAddressesResponse}");
		string? generatedAddress = generateAddressesResponse.Payload?.FirstOrDefault()?.Address;
			
		//Let's request some Shimmer from the faucet
		await account.RequestFromFaucetAsync(generatedAddress);
        
		//Let's Checkout our balance. We will sync the account, followed by checking the balance.
		//Sync the account with the tangle
		await account.SyncAccountAsync();
		//Retrieve balance
		GetBalanceResponse getBalanceResponse = await account.GetBalanceAsync();
		Console.WriteLine($"GetBalanceAsync: {getBalanceResponse}");
		
		//Great, now that we have some test shimmer tokens to send, send to me!
		//Let's send 1 shimmer, which is 1,000,000 Glow, followed by 2 shimmer, which is 2000000 glow, via a single transaction
                //The below creates 2 outputs to the receiver address and 1 more output for your balance.

                string receiverAddress = "rms1qp8rknypruss89dkqnnuedm87y7xmnmdj2tk3rrpcy3sw3ev52q0vzl42tr";

                SendAmountResponse sendAmountResponse = await account.SendAmountUsingBuilder()
                                                                        .AddAddressAndAmount(receiverAddress, 1000000)
                                                                        .AddAddressAndAmount(receiverAddress, 2000000)
                                                                        .SendAmountAsync();


                Console.WriteLine($"SendAmountAsync: {sendAmountResponse}");
}
```

# Examples :heart_eyes:
For more examples, see the [Examples](https://github.com/wireless90/IotaWallet.NET/tree/main/csharp/IotaWalletNet/IotaWalletNet.Main/Examples) directory.

# Supported Commands/Queries :bookmark_tabs:

## Wallet

### Commands

- [x] CreateAccount
- [x] StoreMnemonic
- [x] VerifyMnemonic

### Queries

- [x] GetAccount
- [x] GetAccounts
- [x] GetNewMnemonic


## Account

### Commands

- [x] BuildBasicOutput
- [x] BurnNativeTokens
- [x] BurnNft
- [x] ClaimOutputs
- [x] ConsolidateOutputs
- [x] CreateAliasOutput
- [x] DestroyFoundry
- [x] EnablePeriodicSyncing
- [x] GenerateAddresses
- [x] MeltNativeTokens
- [x] MintNativeTokens
- [x] MintNfts
- [x] RequestFromFaucet
- [x] SendAmount
- [x] SendMicroAmount
- [x] SendNativeTokens
- [x] SendNfts
- [x] SendOutputs
- [x] SyncAccount

### Queries

- [x] GetAddresses
- [x] GetAddressesWithUnspentOutputs
- [x] GetBalance
- [x] GetFoundryOutput
- [x] GetMinimumStorageDepositRequired
- [x] GetOutputs
- [x] GetPendingTransactions
- [x] GetTransaction
- [x] GetTransactions
- [x] GetUnspentOutputs

