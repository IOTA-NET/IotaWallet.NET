using IotaWalletNet.Domain.Common.Models.Account;
using IotaWalletNet.Domain.PlatformInvoke;

namespace IotaWalletNet.Application.AccountContext.Commands.SyncAccount
{
    public partial class SyncAccountCommandHandler
    {
        public class SyncAccountResponse : RustBridgeResponseBase<AccountBalance>
        {

        }
    }
}
