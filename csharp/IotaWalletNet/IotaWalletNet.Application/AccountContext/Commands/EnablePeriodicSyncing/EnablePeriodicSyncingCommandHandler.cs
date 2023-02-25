using MediatR;
using Microsoft.Extensions.Logging;

namespace IotaWalletNet.Application.AccountContext.Commands.EnablePeriodicSyncing
{
    public class EnablePeriodicSyncingCommandHandler : IRequestHandler<EnablePeriodicSyncingCommand>
    {

        public EnablePeriodicSyncingCommandHandler()
        {
        }

        public async Task<Unit> Handle(EnablePeriodicSyncingCommand request, CancellationToken cancellationToken=default)
        {
            while (true)
            {
                Console.WriteLine("SYNCING");
                await request.Account.SyncAccountAsync();

                if (cancellationToken != CancellationToken.None && cancellationToken.IsCancellationRequested)
                    return Unit.Value;

                await Task.Delay(request.IntervalInMilliSeconds);

                if (cancellationToken != CancellationToken.None && cancellationToken.IsCancellationRequested)
                    return Unit.Value;
            }
        }
    }
}
