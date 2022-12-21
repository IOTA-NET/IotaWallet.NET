# Mint NFTs

## Code Example

The following example will:

1. Initialize your account
2. Retrieve your `cookiemonster` account
3. Upload 2 jpg images to ipfs using the sia network (Note you can use any upload method you wish)
4. Create our nft immutable attributes
5. Mint the nfts

```cs
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
                                .ThenBuild()
                            .ConfigureClientOptions()
                                .AddNodeUrl("https://api.testnet.shimmer.network")
                                .SetFaucetUrl("https://faucet.testnet.shimmer.network")
                                .IsFallbackToLocalPow()
                                .IsLocalPow()
                                .ThenBuild()
                            .ConfigureSecretManagerOptions()
                                .SetPassword("password")
                                .SetSnapshotPath("./mystronghold")
                                .ThenBuild()
                            .ThenInitialize();


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
                SiaSkynetClient siaSkynetClient = new SiaSkynetClient();
                foreach (string fileToUpload in filesToUpload)
                {
                    using (FileStream fileStream = File.OpenRead(fileToUpload))
                    {
                        SkyfileResponse skyfileResponse = await siaSkynetClient.UploadFileAsync(fileToUpload, fileStream);
                        urlsOfUploadedFiles.Add($"https://siasky.net/{skyfileResponse.Skylink}");
                    }
                }

                //Lets prepare our nfts
                NFTIRC27 nft1 = new NFTIRC27(KnownMimeTypes.Jpg, "Cats #003", urlsOfUploadedFiles[0])
                                .SetCollectionName("Pussy")
                                .SetDescription("A collection of cats")
                                .SetIssuerName("CookieMonster")
                                .AddAttribute("Colour", "Black")
                                .AddAttribute("Weight", "1kg")
                                .AddInternalAttribute("foreignKey", "OWN1112")
                                .AddInternalAttribute("primaryKey", "PC003");

                NFTIRC27 nft2 = new NFTIRC27(KnownMimeTypes.Jpg, "Cats #004", urlsOfUploadedFiles[1])
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

            }
        }
    }
```
