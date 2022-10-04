using IotaWalletNet.Domain.Common.Models.Account;
using IotaWalletNet.Domain.PlatformInvoke;

namespace IotaWalletNet.Application.WalletContext.Queries.GetAccount
{
    public partial class GetAccountQueryHandler
    {
        public class GetAccountResponse : RustBridgeResponseBase<AccountMeta>
        {

        }
    }
}
