using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.AccountContext.Queries.GetBalance
{
    public class GetBalanceQueryHandler : IRequestHandler<GetBalanceQuery, string>
    {
        public async Task<string> Handle(GetBalanceQuery request, CancellationToken cancellationToken)
        {
            GetBalanceQueryMessage message = new GetBalanceQueryMessage(request.Username);
            string json = JsonConvert.SerializeObject(message);
            string response = await request.Account.SendMessageAsync(json);

            return response;
        }
    }
}
