using IotaWalletNet.Domain.Common.Models.Transaction;
using IotaWalletNet.Domain.PlatformInvoke;
using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.AccountContext.Commands.BurnNativeTokens
{
    public class BurnNativeTokensCommandHandler : IRequestHandler<BurnNativeTokensCommand, BurnNativeTokensResponse>
    {
        public async Task<BurnNativeTokensResponse> Handle(BurnNativeTokensCommand request, CancellationToken cancellationToken)
        {
            BurnNativeTokensCommandMessageData messageData = new BurnNativeTokensCommandMessageData(request.TokenId, request.MeltAmount, new TransactionOptions());

            BurnNativeTokensCommandMessage message = new BurnNativeTokensCommandMessage(request.Username, messageData);

            string jsonMessage = JsonConvert.SerializeObject(message);

            RustBridgeGenericResponse genericResponse = await request.Account.SendMessageAsync(jsonMessage);

            BurnNativeTokensResponse response = genericResponse.IsSuccess
                                                    ? genericResponse.As<BurnNativeTokensResponse>()!
                                                    : new BurnNativeTokensResponse() { Error = genericResponse.As<RustBridgeErrorResponse>(), Type = "error" };

            return response;
        }
    }
}
