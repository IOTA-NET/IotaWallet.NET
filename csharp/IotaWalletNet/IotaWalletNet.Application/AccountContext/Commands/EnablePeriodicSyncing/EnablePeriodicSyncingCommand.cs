using IotaWalletNet.Application.Common.Interfaces;
using MediatR;

namespace IotaWalletNet.Application.AccountContext.Commands.EnablePeriodicSyncing
{
    public class EnablePeriodicSyncingCommand : IRequest
    {
        public EnablePeriodicSyncingCommand(IAccount account, int intervalInMilliSeconds)
        {
            Account = account;
            IntervalInMilliSeconds = intervalInMilliSeconds;
        }

        public IAccount Account { get; set; }

        public int IntervalInMilliSeconds { get; set; }
    }
}
