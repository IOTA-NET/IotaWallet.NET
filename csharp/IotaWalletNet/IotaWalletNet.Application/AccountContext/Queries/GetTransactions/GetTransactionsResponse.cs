using IotaWalletNet.Domain.Common.Models.Transaction;
using IotaWalletNet.Domain.PlatformInvoke;

namespace IotaWalletNet.Application.AccountContext.Queries.GetTransactions
{
    public class GetTransactionsResponse : RustBridgeResponseBase<List<Transaction>>
    {

    }
}
