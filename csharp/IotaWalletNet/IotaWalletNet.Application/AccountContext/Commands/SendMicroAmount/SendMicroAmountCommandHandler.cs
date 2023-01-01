using IotaWalletNet.Domain.Common.Models.Transaction;
using IotaWalletNet.Domain.PlatformInvoke;
using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.AccountContext.Commands.SendMicroAmount
{
    public class SendMicroAmountCommandHandler : IRequestHandler<SendMicroAmountCommand, SendMicroAmountResponse>
    {
        public async Task<SendMicroAmountResponse> Handle(SendMicroAmountCommand request, CancellationToken cancellationToken)
        {
            SendMicroAmountCommandMessageData messageData = new SendMicroAmountCommandMessageData(request.AddressesWithMicroAmount, new TransactionOptions() { TaggedDataPayload = request.TaggedDataPayload });
            SendMicroAmountCommandMessage message = new SendMicroAmountCommandMessage(request.Username, messageData);
            string messageJson = JsonConvert.SerializeObject(message);

            RustBridgeGenericResponse genericResponse = await request.Account.SendMessageAsync(messageJson);

            SendMicroAmountResponse response = genericResponse.As<SendMicroAmountResponse>()!;

            return response;
        }
    }
}
