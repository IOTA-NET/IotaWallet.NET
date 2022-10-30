using IotaWalletNet.Domain.Common.Models.Transaction;
using IotaWalletNet.Domain.PlatformInvoke;

namespace IotaWalletNet.Application.AccountContext.Queries.GetPendingTransactions
{
    public class GetPendingTransactionsResponse : RustBridgeResponseBase<List<Transaction>>
    {

    }
}
