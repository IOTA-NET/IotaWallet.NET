using IotaWalletNet.Domain.PlatformInvoke;
using MediatR;
using Newtonsoft.Json;
using static IotaWalletNet.Application.WalletContext.Queries.GetAccount.GetAccountQueryHandler;

namespace IotaWalletNet.Application.WalletContext.Queries.GetAccounts
{
    public class GetAccountsQueryHandler : IRequestHandler<GetAccountsQuery, GetAccountsResponse>
    {
        public async Task<GetAccountsResponse> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
        {
            GetAccountsQueryMessage message = new GetAccountsQueryMessage();
            string json = JsonConvert.SerializeObject(message);
            RustBridgeGenericResponse genericResponse = await request.Wallet.SendMessageAsync(json);

            GetAccountsResponse response = genericResponse.IsSuccess
                                            ? genericResponse.As<GetAccountsResponse>()!
                                            : new GetAccountsResponse() { Error = genericResponse.As<RustBridgeResponseError>(), Type = "error" };


            return response;
        }
    }
}
