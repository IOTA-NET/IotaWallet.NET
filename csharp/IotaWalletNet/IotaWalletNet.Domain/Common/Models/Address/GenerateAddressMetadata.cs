using IotaWalletNet.Domain.Common.Models.Network;

namespace IotaWalletNet.Domain.Common.Models.Address
{
    public class GenerateAddressMetadata
    {
        public bool Syncing { get; set; }
        public NetworkType Network { get; set; }
    }
}
