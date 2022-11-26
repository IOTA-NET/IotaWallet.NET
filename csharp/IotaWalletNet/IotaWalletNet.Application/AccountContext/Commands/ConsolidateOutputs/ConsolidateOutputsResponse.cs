using IotaWalletNet.Domain.Common.Models.Transaction;
using IotaWalletNet.Domain.PlatformInvoke;

namespace IotaWalletNet.Application.AccountContext.Commands.ConsolidateOutputs
{
    public class ConsolidateOutputsResponse : RustBridgeResponseBase<Transaction>
    {

    }
}
