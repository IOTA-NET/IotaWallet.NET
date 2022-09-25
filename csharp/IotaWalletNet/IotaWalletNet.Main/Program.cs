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
            IServiceCollection services = GetServices();

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            using (IServiceScope scope = serviceProvider.CreateScope())
            {
                IWallet wallet = scope.ServiceProvider.GetRequiredService<IWallet>();

                wallet = ConfigureWallet(wallet);

                wallet.Connect();

                IMediator mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                /* GetNewMnemonicQuery */
                await mediator.Send(new GetNewMnemonicQuery(wallet));

                return;
                /* StoreMnemonicCommand */
                //string mnemonic = "above couple immune stadium enjoy enough sense what december fetch maximum budget chicken memory giant about icon evidence wrestle flash pilot law key embody";
                //await mediator.Send(new StoreMnemonicCommand(wallet, mnemonic));

                /* VerifyMnemonicCommand */
                //string mnemonic = "sail symbol venture people general equal sight pencil slight muscle sausage faculty retreat decorate library all humor metal place mandate cake door disease dwarf";
                //string wrongMnemonic = "venture symbol venture people general equal sight pencil slight muscle sausage faculty retreat decorate library all humor metal place mandate cake door disease dwarf";
                //await mediator.Send(new VerifyMnemonicCommand(wallet, mnemonic));
                //await mediator.Send(new VerifyMnemonicCommand(wallet, wrongMnemonic));



                ///* CreateAccountCommand */
                //await mediator.Send(new CreateAccountCommand(wallet, "zana"));
                //await mediator.Send(new CreateAccountCommand(wallet, "monster"));

                /* GetAccountsQuery */
                //await mediator.Send(new GetAccountsQuery(wallet));

                ///* SyncAccountCommand */
                await mediator.Send(new SyncAccountCommand(wallet, "zana"));

                ///* GetAccountQuery */
                //await mediator.Send(new GetAccountQuery(wallet, "zana"));
                //await mediator.Send(new GetAccountQuery(wallet, "monster"));
                //await mediator.Send(new GetAccountQuery(wallet, "unexisting_username"));

                /* GetBalanceQuery */
                //await mediator.Send(new GetBalanceQuery(wallet, "zana"));

                /* SendAmountCommand */
                string receiverAddress = "rms1qz9f7vecqscfynnxacyzefwvpza0wz3r0lnnwrc8r7qhx65s5x7rx2fln5q";

                var addressesWithOptions = new AddressesWithAmountAndTransactionOptions(new List<AddressWithAmount>() { new AddressWithAmount(receiverAddress, "1000000") });

                await mediator.Send(new SendAmountCommand(wallet, "zana", addressesWithOptions));

                //for(int i = 0; i < 10; i++)
                //    Thread.Sleep(1000);
                /* GetBalanceQuery */
                await mediator.Send(new SyncAccountCommand(wallet, "zana"));
                //await mediator.Send(new GetBalanceQuery(wallet, "zana"));

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

        private static IServiceCollection GetServices()
        {
            return new ServiceCollection()
                                        .AddIotaWalletServices()
                                        .AddMainServices();
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