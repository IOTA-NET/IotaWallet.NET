using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.WalletContext.Queries.GetAccounts
{
    public class GetAccountsQueryHandler : IRequestHandler<GetAccountsQuery>
    {
        public Task<Unit> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
        {
            GetAccountsQueryMessage message = new GetAccountsQueryMessage();
            string messageJson = JsonConvert.SerializeObject(message);
            request.Wallet.SendMessage(messageJson);
            return Unit.Task;
        }
    }
}
