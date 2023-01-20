using IotaWalletNet.Application.Common.Interfaces;
using MediatR;

namespace IotaWalletNet.Application.AccountContext.Commands.EnablePeriodicSyncing
{
    public class EnablePeriodicSyncingCommandHandler : IRequestHandler<EnablePeriodicSyncingCommand, Task>
    {
        public Task<Task> Handle(EnablePeriodicSyncingCommand request, CancellationToken cancellationToken)
        {
            static async Task SyncAndDelay(IAccount account, int interval)
            {
                Console.WriteLine(("Syncing..."));
                await account.SyncAccountAsync();
                await Task.Delay(interval);
            }

            Task periodicSyncingTask = Task.Run(async () =>
            {
                if (request.Count <= 0)
                {
                    while (true)
                        await SyncAndDelay(request.Account, request.IntervalInMilliSeconds);
                }
                else
                {
                    while(request.Count != 0)
                    {
                        await SyncAndDelay(request.Account, request.IntervalInMilliSeconds);
                        request.Count--;
                    }
                }
            });

            return Task.FromResult(periodicSyncingTask);

        }
    }
}
