using IotaWalletNet.Domain.Common.Models.Transaction;
using IotaWalletNet.Domain.PlatformInvoke;
using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.AccountContext.Commands.SendOutputs
{
    public class SendOutputsCommandHandler : IRequestHandler<SendOutputsCommand, SendOutputsResponse>
    {
        public async Task<SendOutputsResponse> Handle(SendOutputsCommand request, CancellationToken cancellationToken)
        {
            TransactionOptions transactionOptions = new TransactionOptions() { TaggedDataPayload = request.TaggedDataPayload };

            SendOutputsCommandMessageData messageData = new SendOutputsCommandMessageData(request.Outputs, transactionOptions);

            SendOutputsCommandMessage message = new SendOutputsCommandMessage(request.Username, messageData);

            string messageJson = JsonConvert.SerializeObject(message);

            RustBridgeGenericResponse rustBridgeGenericResponse = await request.Account.SendMessageAsync(messageJson);

            SendOutputsResponse sendOutputsResponse = rustBridgeGenericResponse.As<SendOutputsResponse>()!;

            return sendOutputsResponse;
        }
    }
}
