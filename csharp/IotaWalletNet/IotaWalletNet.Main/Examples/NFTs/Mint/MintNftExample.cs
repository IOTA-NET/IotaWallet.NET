﻿using IotaWalletNet.Application.AccountContext.Commands.MintNfts;
using IotaWalletNet.Application.Common.Extensions;
using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Domain.Common.Extensions;
using IotaWalletNet.Domain.Common.Models.Coin;
using IotaWalletNet.Domain.Common.Models.Nft;
using Microsoft.Extensions.DependencyInjection;
using MimeMapping;
using Newtonsoft.Json;
using SiaSkynet;
using SiaSkynet.Responses;
using static IotaWalletNet.Application.WalletContext.Queries.GetAccount.GetAccountQueryHandler;

namespace IotaWalletNet.Main.Examples.NFTs.Mint
{
    public static class MintNftExample
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

                string[] filesToUpload = new string[] { "blackcat.jpg", "whitecat.jpg" };
                List<string> urlsOfUploadedFiles = new List<string>();

                //We will be leveraging Sia for free ipfs storage for 90 days
                string siaSkynetPortalUrl = "https://web3portal.com/";
                SiaSkynetClient siaSkynetClient = new SiaSkynetClient(baseUrl: siaSkynetPortalUrl);
                foreach (string fileToUpload in filesToUpload)
                {
                    using (FileStream fileStream = File.OpenRead(fileToUpload))
                    {
                        SkyfileResponse skyfileResponse = await siaSkynetClient.UploadFileAsync(fileToUpload, fileStream);
                        urlsOfUploadedFiles.Add($"{siaSkynetPortalUrl}{skyfileResponse.Skylink}");
                    }
                }

                //Lets prepare our nfts
                NftIrc27 nft1 = new NftIrc27(KnownMimeTypes.Jpg, "Cats #003", urlsOfUploadedFiles[0])
                                .SetCollectionName("Pussy")
                                .SetDescription("A collection of cats")
                                .SetIssuerName("CookieMonster")
                                .AddAttribute("Colour", "Black")
                                .AddAttribute("Weight", "1kg")
                                .AddInternalAttribute("foreignKey", "OWN1112")
                                .AddInternalAttribute("primaryKey", "PC003");

                NftIrc27 nft2 = new NftIrc27(KnownMimeTypes.Jpg, "Cats #004", urlsOfUploadedFiles[1])
                                .SetCollectionName("Pussy")
                                .SetDescription("A collection of cats")
                                .SetIssuerName("CookieMonster")
                                .AddAttribute("Colour", "White")
                                .AddAttribute("Weight", "2kg")
                                .AddInternalAttribute("foreignKey", "OWN3224")
                                .AddInternalAttribute("primaryKey", "PC004");


                //Lets turn our nfts into immutable metadata
                List<NftOptions> nftOptions = new List<NftOptions>()
                {
                    new NftOptions()
                    {
                        Tag = "iotawalletnet".ToHexString(),
                        ImmutableMetadata = JsonConvert.SerializeObject(nft1).ToHexString(),
                    },
                    new NftOptions()
                    {
                        Tag = "iotawalletnet".ToHexString(),
                        ImmutableMetadata = JsonConvert.SerializeObject(nft2).ToHexString(),
                    },
                };

                //Mint our nfts!
                MintNftsResponse mintNftsResponse = await account.MintNftsAsync(nftOptions);
                Console.WriteLine($"MintNftsAsync: {mintNftsResponse}");

                //For testnet
                Console.WriteLine($"Check your block on https://explorer.shimmer.network/testnet/block/{mintNftsResponse.Payload!.BlockId}");

            }
        }
    }
}
