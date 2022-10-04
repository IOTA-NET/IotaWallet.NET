using IotaWalletNet.Domain.PlatformInvoke;

namespace IotaWalletNet.Application.AccountContext.Commands.GenerateAddresses
{
    public class GenerateAddressesResponse : RustBridgeResponseBase<List<GenerateAddressesCommandResponsePayload>>
    {
    }
}
