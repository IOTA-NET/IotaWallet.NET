using IotaWalletNet.Domain.Common.Models.Transaction;
using IotaWalletNet.Domain.PlatformInvoke;

namespace IotaWalletNet.Application.AccountContext.Queries.GetTransaction
{
    public class GetTransactionResponse : RustBridgeResponseBase<Transaction>
    {

    }
}
