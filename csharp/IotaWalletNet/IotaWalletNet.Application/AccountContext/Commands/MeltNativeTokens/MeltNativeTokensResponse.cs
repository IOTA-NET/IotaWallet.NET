using IotaWalletNet.Domain.Common.Models.Transaction;
using IotaWalletNet.Domain.PlatformInvoke;

namespace IotaWalletNet.Application.AccountContext.Commands.MeltNativeTokens
{
    //Response
    public class MeltNativeTokensResponse : RustBridgeResponseBase<Transaction>
    {

    }
}
