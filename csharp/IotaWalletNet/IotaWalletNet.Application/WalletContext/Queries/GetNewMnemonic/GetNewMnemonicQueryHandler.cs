using IotaWalletNet.Domain.PlatformInvoke;
using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.WalletContext.Queries.GetNewMnemonic
{
    public class GetNewMnemonicQueryHandler : IRequestHandler<GetNewMnemonicQuery, GetNewMnemonicResponse>
    {
        public async Task<GetNewMnemonicResponse> Handle(GetNewMnemonicQuery request, CancellationToken cancellationToken)
        {
            GetNewMnemonicQueryMessage message = new GetNewMnemonicQueryMessage();
            string json = JsonConvert.SerializeObject(message);
            RustBridgeGenericResponse genericResponse = await request.Wallet.SendMessageAsync(json);

            GetNewMnemonicResponse response = genericResponse.IsSuccess
                                                                    ? genericResponse.As<GetNewMnemonicResponse>()!
                                                                    : new GetNewMnemonicResponse() { Error = genericResponse.As<RustBridgeResponseError>(), Type = "error" };

            return response;
        }
    }
}
