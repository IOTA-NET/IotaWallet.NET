using IotaWalletNet.Domain.Common.Models.Transaction;
using IotaWalletNet.Domain.PlatformInvoke;
using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.AccountContext.Commands.MeltNativeTokens
{
    public class MeltNativeTokensCommandHandler : IRequestHandler<MeltNativeTokensCommand, MeltNativeTokensResponse>
    {
        public async Task<MeltNativeTokensResponse> Handle(MeltNativeTokensCommand request, CancellationToken cancellationToken)
        {
            //SOLVE use case
            MeltNativeTokensCommandMessageData messageData = new MeltNativeTokensCommandMessageData(request.TokenId, request.MeltAmount, new TransactionOptions());

            MeltNativeTokensCommandMessage message = new MeltNativeTokensCommandMessage(request.Username, messageData);

            string jsonMessage = JsonConvert.SerializeObject(message);

            RustBridgeGenericResponse genericResponse = await request.Account.SendMessageAsync(jsonMessage);

            MeltNativeTokensResponse response = genericResponse.IsSuccess
                                                    ? genericResponse.As<MeltNativeTokensResponse>()!
                                                    : new MeltNativeTokensResponse() { Error = genericResponse.As<RustBridgeErrorResponse>(), Type = "error" };
            return response;
        }
    }
}
