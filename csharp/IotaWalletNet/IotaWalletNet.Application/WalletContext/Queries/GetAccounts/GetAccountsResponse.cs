using IotaWalletNet.Domain.Common.Models.Account;
using IotaWalletNet.Domain.PlatformInvoke;

namespace IotaWalletNet.Application.WalletContext.Queries.GetAccounts
{
    public class GetAccountsResponse : RustBridgeResponseBase<List<AccountMeta>>
    {

    }
}
