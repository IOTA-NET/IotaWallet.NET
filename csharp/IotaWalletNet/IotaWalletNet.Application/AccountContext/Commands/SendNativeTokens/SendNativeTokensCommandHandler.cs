using IotaWalletNet.Domain.Common.Models.Transaction;
using IotaWalletNet.Domain.PlatformInvoke;
using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.AccountContext.Commands.SendNativeTokens
{
    public class SendNativeTokensCommandHandler : IRequestHandler<SendNativeTokensCommand, SendNativeTokensResponse>
    {
        public async Task<SendNativeTokensResponse> Handle(SendNativeTokensCommand request, CancellationToken cancellationToken)
        {
            SendNativeTokensCommandMessageData messageData = new SendNativeTokensCommandMessageData(request.AddressWithNativeTokens, new TransactionOptions());

            SendNativeTokensCommandMessage message = new SendNativeTokensCommandMessage(request.Username, messageData);
            string messageJson = JsonConvert.SerializeObject(message);
            RustBridgeGenericResponse genericResponse = await request.Account.SendMessageAsync(messageJson);

            SendNativeTokensResponse response = genericResponse.IsSuccess
                                                ? genericResponse.As<SendNativeTokensResponse>()!
                                                : new SendNativeTokensResponse() { Error = genericResponse.As<RustBridgeErrorResponse>(), Type = "error" };
            return response;
        }
    }
}
