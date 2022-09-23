using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.AccountContext.Queries.GetBalance
{
    public class GetBalanceQueryHandler : IRequestHandler<GetBalanceQuery>
    {
        public Task<Unit> Handle(GetBalanceQuery request, CancellationToken cancellationToken)
        {
            GetBalanceQueryMessage message = new GetBalanceQueryMessage(request.Username);
            string json = JsonConvert.SerializeObject(message);
            request.Wallet.SendMessage(json);

            return Unit.Task;
        }
    }
}
