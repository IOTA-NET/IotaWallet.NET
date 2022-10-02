using IotaWalletNet.Domain.PlatformInvoke;
using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.WalletContext.Queries.GetAccount
{
    public class GetAccountQueryHandler : IRequestHandler<GetAccountQuery, string>
    {
        public async Task<string> Handle(GetAccountQuery request, CancellationToken cancellationToken)
        {
            GetAccountQueryMessage message = new GetAccountQueryMessage(request.Username);
            string json = JsonConvert.SerializeObject(message);
            RustBridgeGenericResponse response = await request.Wallet.SendMessageAsync(json);

            return "";
        }
    }
}
