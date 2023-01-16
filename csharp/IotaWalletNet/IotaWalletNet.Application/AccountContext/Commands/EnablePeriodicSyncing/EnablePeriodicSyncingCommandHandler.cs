using MediatR;

namespace IotaWalletNet.Application.AccountContext.Commands.EnablePeriodicSyncing
{
    public class EnablePeriodicSyncingCommandHandler : IRequestHandler<EnablePeriodicSyncingCommand, Task>
    {
        public Task<Task> Handle(EnablePeriodicSyncingCommand request, CancellationToken cancellationToken)
        {
            Task periodicSyncingTask = Task.Run(async () =>
            {
                await request.Account.SyncAccountAsync();
                await Task.Delay(request.IntervalInMilliSeconds);
            });

            return Task.FromResult(periodicSyncingTask);
        }
    }
}
