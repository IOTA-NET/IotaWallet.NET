using IotaWalletNet.Domain.Common.Models.Transaction;
using IotaWalletNet.Domain.PlatformInvoke;

namespace IotaWalletNet.Application.AccountContext.Commands.BurnNft
{
    public class BurnNftResponse : RustBridgeResponseBase<Transaction>
    {

    }
}
