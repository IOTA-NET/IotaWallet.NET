using IotaWalletNet.Domain.PlatformInvoke;
using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.AccountContext.Queries.GetTransactions
{
    public class GetTransactionsQueryHandler : IRequestHandler<GetTransactionsQuery, GetTransactionsResponse>
    {
        public async Task<GetTransactionsResponse> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
        {
            GetTransactionsQueryMessage message = new GetTransactionsQueryMessage(request.Username);
            string json = JsonConvert.SerializeObject(message);

            RustBridgeGenericResponse genericResponse = await request.Account.SendMessageAsync(json);

            GetTransactionsResponse response = genericResponse.IsSuccess
                                                ? genericResponse.As<GetTransactionsResponse>()!
                                                : new GetTransactionsResponse() { Error = genericResponse.As<RustBridgeErrorResponse>(), Type = "error" };
            
            return response;
        }
    }
}
