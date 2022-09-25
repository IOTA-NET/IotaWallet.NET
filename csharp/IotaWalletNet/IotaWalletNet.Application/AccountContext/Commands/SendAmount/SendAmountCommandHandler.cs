using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.AccountContext.Commands.SendAmount
{
    public class SendAmountCommandHandler : IRequestHandler<SendAmountCommand, string>
    {
        public async Task<string> Handle(SendAmountCommand request, CancellationToken cancellationToken)
        {
            SendAmountCommandMessage message = new SendAmountCommandMessage(request.Username, request.AddressesWithAmountAndTransactionOptions);
            string json = JsonConvert.SerializeObject(message);
            string response = await request.Wallet.SendMessageAsync(json);

            return response;
        }
    }
}
