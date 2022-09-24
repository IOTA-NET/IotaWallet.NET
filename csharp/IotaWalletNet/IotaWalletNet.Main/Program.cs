using IotaWalletNet.Application.AccountContext.Commands.SyncAccount;
using IotaWalletNet.Application.AccountContext.Queries.GetBalance;
using IotaWalletNet.Application.Common.Extensions;
using IotaWalletNet.Application.WalletContext.Commands.CreateAccount;
using IotaWalletNet.Application.WalletContext.Commands.StoreMnemonic;
using IotaWalletNet.Application.WalletContext.Queries.GetAccount;
using IotaWalletNet.Application.WalletContext.Queries.GetNewMnemonic;
using IotaWalletNet.Domain.Common.Extensions;
using IotaWalletNet.Domain.Common.Interfaces;
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
                //await mediator.Send(new GetNewMnemonicQuery(wallet));

                /* StoreMnemonicCommand */
                //string mnemonic = "wide believe journey slow share surround depth tube black behave again pigeon pencil arch deliver napkin exhibit sphere artwork pumpkin second vague round robust";
                //await mediator.Send(new StoreMnemonicCommand(wallet, mnemonic));

                /* VerifyMnemonicCommand */
                //string mnemonic = "sail symbol venture people general equal sight pencil slight muscle sausage faculty retreat decorate library all humor metal place mandate cake door disease dwarf";
                //string wrongMnemonic = "venture symbol venture people general equal sight pencil slight muscle sausage faculty retreat decorate library all humor metal place mandate cake door disease dwarf";
                //await mediator.Send(new VerifyMnemonicCommand(wallet, mnemonic));
                //await mediator.Send(new VerifyMnemonicCommand(wallet, wrongMnemonic));



                /* CreateAccountCommand */
                //await mediator.Send(new CreateAccountCommand(wallet, "zana"));
                //await mediator.Send(new CreateAccountCommand(wallet, "monster"));

                /* GetAccountsQuery */
                //await mediator.Send(new GetAccountsQuery(wallet));

                /* GetAccountQuery */
                //await mediator.Send(new GetAccountQuery(wallet, "zana"));
                //await mediator.Send(new GetAccountQuery(wallet, "monster"));
                //await mediator.Send(new GetAccountQuery(wallet, "unexisting_username"));

                /* SyncAccountCommand */
                await mediator.Send(new SyncAccountCommand(wallet, "zana"));

                /* GetBalanceQuery */
                await mediator.Send(new GetBalanceQuery(wallet, "zana"));

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