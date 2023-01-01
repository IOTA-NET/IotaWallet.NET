using IotaWalletNet.Domain.Common.Models.Transaction;
using IotaWalletNet.Domain.PlatformInvoke;

namespace IotaWalletNet.Application.AccountContext.Commands.SendMicroAmount
{
    public class SendMicroAmountResponse : RustBridgeResponseBase<Transaction>
    {

    }
}
