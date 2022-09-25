using IotaWalletNet.Application.AccountContext.Commands.SendAmount;
using IotaWalletNet.Application.AccountContext.Commands.SyncAccount;
using IotaWalletNet.Application.AccountContext.Queries.GetBalance;
using IotaWalletNet.Application.Common.Extensions;
using IotaWalletNet.Application.WalletContext.Commands.CreateAccount;
using IotaWalletNet.Application.WalletContext.Commands.StoreMnemonic;
using IotaWalletNet.Application.WalletContext.Queries.GetAccount;
using IotaWalletNet.Application.WalletContext.Queries.GetNewMnemonic;
using IotaWalletNet.Domain.Common.Extensions;
using IotaWalletNet.Domain.Common.Interfaces;
using IotaWalletNet.Domain.Common.Models.Address;
using IotaWalletNet.Domain.Options;
using IotaWalletNet.Main.Common.Extensions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace IotaWalletNet.Main
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection().AddIotaWalletServices();

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            using (IServiceScope scope = serviceProvider.CreateScope())
            {
                IWallet wallet = scope.ServiceProvider.GetRequiredService<IWallet>();

                wallet = ConfigureWallet(wallet);

                wallet.Connect();

                IMediator mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                string response = String.Empty;
                
                ///* GetNewMnemonicQuery */
                //response = await wallet.GetNewMnemonicAsync();

                ///* StoreMnemonicCommand */
                //string mnemonic = "above couple immune stadium enjoy enough sense what december fetch maximum budget chicken memory giant about icon evidence wrestle flash pilot law key embody";
                //response = await wallet.StoreMnemonicAsync(mnemonic);

                ///* VerifyMnemonicCommand */
                //string wrongMnemonic = "venture symbol venture people general equal sight pencil slight muscle sausage faculty retreat decorate library all humor metal place mandate cake door disease dwarf";
                //response = await wallet.VerifyMnemonicAsync(wrongMnemonic);
                //response = await wallet.VerifyMnemonicAsync(mnemonic);


                /////* CreateAccountCommand */
                //response = await wallet.CreateAccountAsync("zana");
                //response = await wallet.CreateAccountAsync("monster");

                
                /////* SyncAccountCommand */
                //await mediator.Send(new SyncAccountCommand(wallet, "zana"));

                /////* GetAccountQuery */
                response = await wallet.GetAccountAsync("zana");
                //response = await wallet.GetAccountAsync("monster");
                //response = await wallet.GetAccountAsync("unexisting_username");

                ///* GetBalanceQuery */
                //response = await wallet.GetBalanceAsync("zana");

                ///* SendAmountCommand */
                //string receiverAddress = "rms1qz9f7vecqscfynnxacyzefwvpza0wz3r0lnnwrc8r7qhx65s5x7rx2fln5q";
                //var addressesWithOptions = new AddressesWithAmountAndTransactionOptions(new List<AddressWithAmount>() { new AddressWithAmount(receiverAddress, "1000000") });
                //response = await wallet.SendAmountAsync("zana", addressesWithOptions);
               

                //Alternatively send a raw message
                //wallet.SendMessage(@"
                //{
                //    ""cmd"": ""StoreMnemonic"",
                //    ""payload"": mnemonic
                //}
                //");

                Console.Read();


            }

        }


        private static IWallet ConfigureWallet(IWallet wallet)
        {
            return wallet
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
        }
    }
}