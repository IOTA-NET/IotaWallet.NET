using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.WalletContext.Queries.GetAccounts
{
    public class GetAccountsQueryHandler : IRequestHandler<GetAccountsQuery, string>
    {
        public async Task<string> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
        {
            GetAccountsQueryMessage message = new GetAccountsQueryMessage();
            string json = JsonConvert.SerializeObject(message);
            string response = await request.Wallet.SendMessageAsync(json);
            return response;
        }
    }
}
