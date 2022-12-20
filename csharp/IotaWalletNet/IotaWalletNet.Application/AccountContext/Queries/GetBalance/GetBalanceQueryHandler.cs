using IotaWalletNet.Domain.PlatformInvoke;
using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.AccountContext.Queries.GetBalance
{
    public class GetBalanceQueryHandler : IRequestHandler<GetBalanceQuery, GetBalanceResponse>
    {
        public async Task<GetBalanceResponse> Handle(GetBalanceQuery request, CancellationToken cancellationToken)
        {
            GetBalanceQueryMessage message = new GetBalanceQueryMessage(request.Username);
            string json = JsonConvert.SerializeObject(message);
            RustBridgeGenericResponse genericResponse = await request.Account.SendMessageAsync(json);

            GetBalanceResponse response = genericResponse.As<GetBalanceResponse>()!;

            return response;
        }
    }
}
