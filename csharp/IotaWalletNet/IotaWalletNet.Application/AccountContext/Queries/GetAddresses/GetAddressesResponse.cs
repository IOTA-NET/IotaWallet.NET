using IotaWalletNet.Domain.Common.Models.Address;
using IotaWalletNet.Domain.PlatformInvoke;

namespace IotaWalletNet.Application.AccountContext.Queries.GetAddresses
{
    public class GetAddressesResponse : RustBridgeResponseBase<List<AccountAddress>>
    {

    }
}
