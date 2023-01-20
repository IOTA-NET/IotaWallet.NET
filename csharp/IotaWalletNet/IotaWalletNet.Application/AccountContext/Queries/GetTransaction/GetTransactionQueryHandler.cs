using IotaWalletNet.Domain.PlatformInvoke;
using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.AccountContext.Queries.GetTransaction
{
    public class GetTransactionQueryHandler : IRequestHandler<GetTransactionQuery, GetTransactionResponse>
    {
        public async Task<GetTransactionResponse> Handle(GetTransactionQuery request, CancellationToken cancellationToken)
        {
            GetTransactionQueryMessageData messageData = new GetTransactionQueryMessageData(request.TransactionId);
            GetTransactionQueryMessage message = new GetTransactionQueryMessage(request.Username, messageData);
            string json = JsonConvert.SerializeObject(message);

            RustBridgeGenericResponse genericResponse = await request.Account.SendMessageAsync(json);

            GetTransactionResponse response = genericResponse.As<GetTransactionResponse>()!;
            return response;
        }
    }
}
