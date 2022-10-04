using IotaWalletNet.Application.WalletContext.Commands.CreateAccount;
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
            GetAccountQueryMessage message = new GetAccountQueryMessage(request.Username);
            string json = JsonConvert.SerializeObject(message);
            RustBridgeGenericResponse genericResponse = await request.Wallet.SendMessageAsync(json);

            GetAccountResponse response = genericResponse.IsSuccess
                                            ? genericResponse.As<GetAccountResponse>()!
                                            : new GetAccountResponse() { Error = genericResponse.As<RustBridgeResponseError>(), Type = "error" };


            return response;
        }
    }
}
