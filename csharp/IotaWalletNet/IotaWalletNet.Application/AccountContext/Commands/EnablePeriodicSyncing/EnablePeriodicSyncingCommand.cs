using IotaWalletNet.Application.Common.Interfaces;
using MediatR;

namespace IotaWalletNet.Application.AccountContext.Commands.EnablePeriodicSyncing
{
    public class EnablePeriodicSyncingCommand : IRequest<Task>
    {
        public EnablePeriodicSyncingCommand(IAccount account, int intervalInMilliSeconds, int count)
        {
            Account = account;
            IntervalInMilliSeconds = intervalInMilliSeconds;
            Count = count;
        }

        public IAccount Account { get; set; }

        public int IntervalInMilliSeconds { get; set; }
        public int Count { get; set; }
    }
}
