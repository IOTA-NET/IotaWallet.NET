using IotaWalletNet.Domain.Common.Models.Transaction;
using IotaWalletNet.Domain.PlatformInvoke;

namespace IotaWalletNet.Application.AccountContext.Commands.SendNativeTokens
{
    public class SendNativeTokensResponse : RustBridgeResponseBase<Transaction>
    {
    }
}
