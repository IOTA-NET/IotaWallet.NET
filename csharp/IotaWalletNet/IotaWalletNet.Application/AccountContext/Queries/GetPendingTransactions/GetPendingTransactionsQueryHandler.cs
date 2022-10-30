using IotaWalletNet.Domain.PlatformInvoke;
using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.AccountContext.Queries.GetPendingTransactions
{
    public class GetPendingTransactionsQueryHandler : IRequestHandler<GetPendingTransactionsQuery, GetPendingTransactionsResponse>
    {
        public async Task<GetPendingTransactionsResponse> Handle(GetPendingTransactionsQuery request, CancellationToken cancellationToken)
        {
            GetPendingTransactionsQueryMessage message = new GetPendingTransactionsQueryMessage(request.Username);
            string json = JsonConvert.SerializeObject(message);

            RustBridgeGenericResponse genericResponse = await request.Account.SendMessageAsync(json);

            GetPendingTransactionsResponse response = genericResponse.IsSuccess
                                                        ? genericResponse.As<GetPendingTransactionsResponse>()!
                                                        : new GetPendingTransactionsResponse() { Error = genericResponse.As<RustBridgeErrorResponse>(), Type = "error" };

            return response;
        }
    }
}
