using IotaWalletNet.Application.AccountContext.Commands.SendNativeTokens;
using IotaWalletNet.Application.AccountContext.Commands.SyncAccount;
using IotaWalletNet.Application.AccountContext.Queries.GetBalance;
using IotaWalletNet.Application.Common.Extensions;
using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Domain.Common.Extensions;
using IotaWalletNet.Domain.Common.Models.Account;
using IotaWalletNet.Domain.Common.Models.Address;
using IotaWalletNet.Domain.Common.Models.Coin;
using Microsoft.Extensions.DependencyInjection;
using static IotaWalletNet.Application.WalletContext.Queries.GetAccount.GetAccountQueryHandler;

namespace IotaWalletNet.Main.Examples.Native_Tokens.Send
{
    public class SendNativeTokensExample
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


                SyncAccountResponse syncAccountResponse = await account.SyncAccountAsync();
                Console.WriteLine($"SyncAccountAsync: {syncAccountResponse}");

                GetBalanceResponse getBalanceResponse = await account.GetBalanceAsync();
                Console.WriteLine($"GetBalanceAsync: {getBalanceResponse}");

                /*
                 * You can get a specific native token by looking at the tokenId.
                 * For this example, we are just getting the first native token available which has atleast 10 tokens,
                 * since we are intending to send 10 tokens.
                 * */
                NativeTokenBalance? nativeTokenBalance = getBalanceResponse.Payload?.NativeTokens?.First(nativeTokenBalance => nativeTokenBalance.Available.FromHexEncodedAmountToUInt64() > 10);

                if (nativeTokenBalance == null)
                {
                    throw new Exception("No native tokens found that has atleast 10 tokens available...");
                }

                string tokenId = nativeTokenBalance.TokenId;
                string amount = 10.ToHexEncodedAmount();

                string receiverAddress = "rms1qrcagm98yyj983aan86wvvlgv8g72zspvhv40eynpmdn4ms8rmvrgkfqvfp";
                AddressWithNativeTokens addressWithNativeTokens = new AddressWithNativeTokens(new List<string[]> { new string[] { tokenId, amount } }.ToList(), receiverAddress);
                SendNativeTokensResponse sendNativeTokensResponse = await account.SendNativeTokensAsync(new AddressWithNativeTokens[] { addressWithNativeTokens }.ToList());
                Console.WriteLine($"SendNativeTokensAsync: {addressWithNativeTokens}");

                //For testnet
                Console.WriteLine($"Check your block on https://explorer.shimmer.network/testnet/block/{sendNativeTokensResponse.Payload!.BlockId}");

            }
        }

    }
}
