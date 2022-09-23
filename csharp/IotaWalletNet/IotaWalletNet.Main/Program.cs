
using IotaWalletNet.Application.Common.Extensions;
using IotaWalletNet.Application.WalletContext.Commands.CreateAccount;
using IotaWalletNet.Application.WalletContext.Commands.StoreMnemonic;
using IotaWalletNet.Application.WalletContext.Queries.GetAccount;
using IotaWalletNet.Application.WalletContext.Queries.GetAccounts;
using IotaWalletNet.Domain;
using IotaWalletNet.Domain.Common.Extensions;
using IotaWalletNet.Domain.Common.Interfaces;
using IotaWalletNet.Domain.Options;
using IotaWalletNet.Testbed.Common.Extensions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace IotaWalletNet.Testbed
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

                //string mnemonic = "sail symbol venture people general equal sight pencil slight muscle sausage faculty retreat decorate library all humor metal place mandate cake door disease dwarf";

                //await mediator.Send(new StoreMnemonicCommand(wallet, mnemonic));
                //await mediator.Send(new CreateAccountCommand(wallet, "zana"));
                //await mediator.Send(new CreateAccountCommand(wallet, "monster"));
                //await mediator.Send(new GetAccountsQuery(wallet));

                await mediator.Send(new GetAccountQuery(wallet, "zana"));
                await mediator.Send(new GetAccountQuery(wallet, "monster"));
                await mediator.Send(new GetAccountQuery(wallet, "unexisting_username"));


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
                                        .AddDomainServices()
                                        .AddApplicationServices()
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
                        .IsLocalPow(false)
                        .ThenBuild()
                    .ConfigureSecretManagerOptions()
                        .SetPassword("password")
                        .SetSnapshotPath("./mystronghold")
                        .ThenBuild();
        }
    }
}