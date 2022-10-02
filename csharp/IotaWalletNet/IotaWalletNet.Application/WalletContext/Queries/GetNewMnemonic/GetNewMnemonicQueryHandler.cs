using IotaWalletNet.Domain.PlatformInvoke;
using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.WalletContext.Queries.GetNewMnemonic
{
    public class GetNewMnemonicQueryHandler : IRequestHandler<GetNewMnemonicQuery, GetNewMnemonicQueryResponse>
    {
        public async Task<GetNewMnemonicQueryResponse> Handle(GetNewMnemonicQuery request, CancellationToken cancellationToken)
        {
            GetNewMnemonicQueryMessage message = new GetNewMnemonicQueryMessage();
            string json = JsonConvert.SerializeObject(message);
            RustBridgeGenericResponse jsonResponse = await request.Wallet.SendMessageAsync(json);
            //GetNewMnemonicQueryResponse response = JsonConvert.DeserializeObject<GetNewMnemonicQueryResponse>(jsonResponse);

            return new GetNewMnemonicQueryResponse();
        }
    }
}
