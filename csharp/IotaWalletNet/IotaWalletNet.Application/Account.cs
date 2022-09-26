using IotaWalletNet.Application.AccountContext.Commands.SendAmount;
using IotaWalletNet.Application.AccountContext.Commands.SyncAccount;
using IotaWalletNet.Application.AccountContext.Queries.GetBalance;
using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Domain.Common.Models.Address;
using IotaWalletNet.Domain.PlatformInvoke;
using MediatR;

namespace IotaWalletNet.Application
{
    public class Account : RustBridgeCommunicator, IAccount
    {
        private readonly IMediator _mediator;

        public Account(IMediator mediator, string username, IWallet wallet)
        {
            _mediator = mediator;
            Username = username;
            Wallet = wallet;
        }

        public string Username { get; }
        public IWallet Wallet { get; }

        public async Task<string> GetBalanceAsync(string username)
        {
            return await _mediator.Send(new GetBalanceQuery(this, username));
        }

        public async Task<string> GetBalanceAsync()
        {
            return await _mediator.Send(new GetBalanceQuery(this, Username));
        }

        public async Task<string> SendAmountAsync(AddressesWithAmountAndTransactionOptions addressesWithAmountAndTransactionOptions)
        {
            return await _mediator.Send(new SendAmountCommand(this, Username, addressesWithAmountAndTransactionOptions));
        }

        public async Task<string> SyncAccountAsync()
        {
            return await _mediator.Send(new SyncAccountCommand(this, Username));
        }
    }
}
