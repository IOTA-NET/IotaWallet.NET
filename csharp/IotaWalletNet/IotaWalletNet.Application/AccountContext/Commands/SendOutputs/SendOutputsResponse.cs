using IotaWalletNet.Domain.Common.Models.Transaction;
using IotaWalletNet.Domain.PlatformInvoke;

namespace IotaWalletNet.Application.AccountContext.Commands.SendOutputs
{
    //Response
    public class SendOutputsResponse : RustBridgeResponseBase<Transaction>
    {

    }
}
