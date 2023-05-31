using IotaWalletNet.Domain.PlatformInvoke;
using MediatR;
using Newtonsoft.Json;
using static IotaWalletNet.Application.WalletContext.Queries.GetAccount.GetAccountQueryHandler;

namespace IotaWalletNet.Application.WalletContext.Queries.GetAccount
{
    public partial class GetAccountQueryHandler : IRequestHandler<GetAccountQuery, GetAccountResponse>
    {
        public async Task<GetAccountResponse> Handle(GetAccountQuery request, CancellationToken cancellationToken)
        {
            GetAccountQueryMessageData messageData = new GetAccountQueryMessageData(request.Username);
            GetAccountQueryMessage message = new GetAccountQueryMessage(messageData);
            string json = JsonConvert.SerializeObject(message);
            RustBridgeGenericResponse genericResponse = await request.Wallet.SendMessageAsync(json);

            GetAccountResponse response = genericResponse.As<GetAccountResponse>()!;

            return response;
        }
    }
}
