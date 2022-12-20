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
            MintNativeTokensCommandMessage message = new MintNativeTokensCommandMessage(request.Username, messageData);
            string messageJson = JsonConvert.SerializeObject(message);

            RustBridgeGenericResponse genericResponse = await request.Account.SendMessageAsync(messageJson);

            MintNativeTokensResponse response = genericResponse.As<MintNativeTokensResponse>()!;
            return response;
        }
    }

}
