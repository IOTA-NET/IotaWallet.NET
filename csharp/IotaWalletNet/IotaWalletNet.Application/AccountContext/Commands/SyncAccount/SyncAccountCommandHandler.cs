using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.AccountContext.Commands.SyncAccount
{
    public class SyncAccountCommandHandler : IRequestHandler<SyncAccountCommand>
    {
        public Task<Unit> Handle(SyncAccountCommand request, CancellationToken cancellationToken)
        {
            SyncAccountCommandMessage message = new SyncAccountCommandMessage(request.Username, new AccountSyncOptions());
            string json = JsonConvert.SerializeObject(message);
            request.Wallet.SendMessage(json);

            return Unit.Task;
        }
    }
}
