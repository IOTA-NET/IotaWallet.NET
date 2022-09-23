using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.WalletContext.Queries.GetAccount
{
    public class GetAccountQueryHandler : IRequestHandler<GetAccountQuery>
    {
        public Task<Unit> Handle(GetAccountQuery request, CancellationToken cancellationToken)
        {
            GetAccountQueryMessage message = new GetAccountQueryMessage(request.Username);
            string json = JsonConvert.SerializeObject(message);
            request.Wallet.SendMessage(json);

            return Unit.Task;
        }
    }
}
