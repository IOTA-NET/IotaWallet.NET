using IotaWalletNet.Domain.Common.Interfaces;
using IotaWalletNet.Domain.Common.Models.Address;
using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.AccountContext.Commands.SendAmount
{
    public class SendAmountCommand : IRequest
    {
        public SendAmountCommand(IWallet wallet, string username, AddressesWithAmountAndTransactionOptions addressesWithAmountAndTransactionOptions)
        {
            Wallet = wallet;
            Username = username;
            AddressesWithAmountAndTransactionOptions = addressesWithAmountAndTransactionOptions;
        }

        public IWallet Wallet { get; }
        public string Username { get; }
        public AddressesWithAmountAndTransactionOptions AddressesWithAmountAndTransactionOptions { get; }
    }

    public class SendAmountCommandHandler : IRequestHandler<SendAmountCommand>
    {
        public Task<Unit> Handle(SendAmountCommand request, CancellationToken cancellationToken)
        {
            SendAmountCommandMessage message = new SendAmountCommandMessage(request.Username, request.AddressesWithAmountAndTransactionOptions);
            string json = JsonConvert.SerializeObject(message);
            request.Wallet.SendMessage(json);

            return Unit.Task;
        }
    }
}
