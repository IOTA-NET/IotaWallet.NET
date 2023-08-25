using IotaWalletNet.Application.AccountContext.Commands.BuildBasicOutput;
using IotaWalletNet.Application.AccountContext.Commands.BuildNftOutput;
using IotaWalletNet.Application.AccountContext.Commands.SendOutputs;
using IotaWalletNet.Application.Common.Extensions;
using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Domain.Common.Extensions;
using IotaWalletNet.Domain.Common.Interfaces;
using IotaWalletNet.Domain.Common.Models.Address.AddressTypes;
using IotaWalletNet.Domain.Common.Models.Coin;
using IotaWalletNet.Domain.Common.Models.Network;
using IotaWalletNet.Domain.Common.Models.Output.FeatureTypes;
using IotaWalletNet.Domain.Common.Models.Output.OutputDataTypes;
using IotaWalletNet.Domain.Common.Models.Output.OutputTypes;
using IotaWalletNet.Domain.Common.Models.Output.UnlockConditionTypes;
using Microsoft.Extensions.DependencyInjection;
using static IotaWalletNet.Application.WalletContext.Queries.GetAccount.GetAccountQueryHandler;

namespace IotaWalletNet.Main.Examples.NFTs.Mint
{
    public static class MintNftUsingBuildOutputExample
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

                //Sync account
                await account.SyncAccountAsync();
                string walletAddress = (await account.GetAddressesAsync()).Payload!.First().Address;

                //Ed25519Address ed25519Address = new Ed25519Address(walletAddress.DecodeBech32(NetworkType.Testnet, TypeOfCoin.Shimmer));
                //AddressUnlockCondition addressUnlockCondition = new AddressUnlockCondition(ed25519Address);

                //BuildBasicOutputData buildBasicOutputData = new BuildBasicOutputData(
                //                                                    amount: "1000000", 
                //                                                    nativeTokens: null,
                //                                                    unlockConditions: new() { addressUnlockCondition },
                //                                                    features: null);
                //BuildBasicOutputResponse buildBasicOutputResponse =  await account.BuildBasicOutputAsync(buildBasicOutputData);
                //BasicOutput basicOutput = buildBasicOutputResponse.Payload!;

                //SendOutputsResponse sendOutputsResponse = await account.SendOutputsAsync(new() { basicOutput });
                //string transactionId = sendOutputsResponse.Payload!.TransactionId;

                Ed25519Address ed25519Address = new Ed25519Address(walletAddress.DecodeBech32(NetworkType.Testnet, TypeOfCoin.Shimmer));

                string? amount = "500000";

                List<IFeatureType>? features = null;

                IssuerFeature issuerFeature = new IssuerFeature(ed25519Address);
                MetadataFeature immutableMetadataFeature = new MetadataFeature("Hello".ToHexString());
                List<IFeatureType> immutableFeatures = new List<IFeatureType> { issuerFeature, immutableMetadataFeature };

                NativeToken? nativeToken = null;

                AddressUnlockCondition addressUnlockCondition = new AddressUnlockCondition(ed25519Address);
                StorageDepositReturnUnlockCondition storageDepositReturnUnlockCondition = new StorageDepositReturnUnlockCondition(ed25519Address, amount);
                List<IUnlockConditionType> unlockConditions = new List<IUnlockConditionType>() { addressUnlockCondition, storageDepositReturnUnlockCondition };

                BuildNftOutputData buildNftOutputData = new BuildNftOutputData(amount, nativeToken, unlockConditions, null, immutableFeatures, "0x0000000000000000000000000000000000000000000000000000000000000000");
                BuildNftOutputResponse buildNftOutputResponse = await account.BuildNftOutputAsync(buildNftOutputData);
                NftOutput nftOutput = buildNftOutputResponse.Payload!;
                SendOutputsResponse sendOutputsResponse = await account.SendOutputsAsync(new List<IOutputType>() { nftOutput });
                string transactionId = sendOutputsResponse.Payload!.TransactionId;

            }
        }

    }
}
