using IotaWalletNet.Domain.PlatformInvoke;
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
            RustBridgeGenericResponse response = await request.Account.SendMessageAsync(json);

            return "";
        }
    }
}
