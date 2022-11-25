using IotaWalletNet.Domain.Common.Models.Address;
using IotaWalletNet.Domain.Common.Models.Transaction;
using IotaWalletNet.Domain.PlatformInvoke;
using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.AccountContext.Commands.SendAmount
{
    public class SendAmountCommandHandler : IRequestHandler<SendAmountCommand, SendAmountResponse>
    {
        public async Task<SendAmountResponse> Handle(SendAmountCommand request, CancellationToken cancellationToken)
        {
            SendAmountCommandMessageData messageData = new SendAmountCommandMessageData(request.AddressesWithAmount, new TransactionOptions());
            SendAmountCommandMessage message = new SendAmountCommandMessage(request.Username, messageData);
            string json = JsonConvert.SerializeObject(message);

            RustBridgeGenericResponse genericResponse = await request.Account.SendMessageAsync(json);

            SendAmountResponse response = genericResponse.IsSuccess
                                            ? genericResponse.As<SendAmountResponse>()!
                                            : new SendAmountResponse() { Error = genericResponse.As<RustBridgeErrorResponse>(), Type = "error" };


            return response;
        }
    }
}
