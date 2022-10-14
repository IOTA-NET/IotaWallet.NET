using IotaWalletNet.Domain.Common.Models.Address;
using IotaWalletNet.Domain.PlatformInvoke;

namespace IotaWalletNet.Application.AccountContext.Queries.GetAddressesWithUnspentOutputs
{
    public class GetAddressesWithUnspentOutputsResponse : RustBridgeResponseBase<List<AddressWithUnspentOutputs>>
    {

    }
}
