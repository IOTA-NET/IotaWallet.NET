using IotaWalletNet.Domain.Common.Models.Account;
using IotaWalletNet.Domain.PlatformInvoke;

namespace IotaWalletNet.Application.AccountContext.Queries.GetBalance
{
    public class GetBalanceResponse : RustBridgeResponseBase<AccountBalance>
    {

    }
}
