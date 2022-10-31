using IotaWalletNet.Domain.Common.Models.Transaction;
using IotaWalletNet.Domain.PlatformInvoke;
using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.AccountContext.Commands.MintNativeTokens
{
    public class MintNativeTokensCommandHandler : IRequestHandler<MintNativeTokensCommand, MintNativeTokensResponse>
    {
        public async Task<MintNativeTokensResponse> Handle(MintNativeTokensCommand request, CancellationToken cancellationToken)
        {
            MintNativeTokensCommandMessageData messageData = new MintNativeTokensCommandMessageData(request.NativeTokenOptions, new TransactionOptions());
            MintNAtiveTokensCommandMessage message = new MintNAtiveTokensCommandMessage(messageData);
            string messageJson = JsonConvert.SerializeObject(message);

            RustBridgeGenericResponse genericResponse = await request.Account.SendMessageAsync(messageJson);

            MintNativeTokensResponse response = genericResponse.IsSuccess
                                                    ? genericResponse.As<MintNativeTokensResponse>()!
                                                    : new MintNativeTokensResponse() { Error = genericResponse.As<RustBridgeErrorResponse>(), Type = "error" };

            return response;
        }
    }

}
