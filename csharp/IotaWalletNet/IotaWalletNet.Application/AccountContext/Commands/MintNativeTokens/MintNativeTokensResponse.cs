using IotaWalletNet.Domain.Common.Models.Transaction;
using IotaWalletNet.Domain.PlatformInvoke;

namespace IotaWalletNet.Application.AccountContext.Commands.MintNativeTokens
{
    public class MintNativeTokensResponse : RustBridgeResponseBase<MintTokenTransaction>
    {

    }

}
