using IotaWalletNet.Domain.Common.Models.Transaction;
using IotaWalletNet.Domain.PlatformInvoke;

namespace IotaWalletNet.Application.AccountContext.Commands.SendNfts
{
    public class SendNftsResponse : RustBridgeResponseBase<Transaction>
    {

    }
}
