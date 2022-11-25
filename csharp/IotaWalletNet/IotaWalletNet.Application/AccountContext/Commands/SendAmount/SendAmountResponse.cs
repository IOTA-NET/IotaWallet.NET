using IotaWalletNet.Domain.Common.Models.Transaction;
using IotaWalletNet.Domain.PlatformInvoke;

namespace IotaWalletNet.Application.AccountContext.Commands.SendAmount
{
    public class SendAmountResponse : RustBridgeResponseBase<Transaction>
    {

    }
}
