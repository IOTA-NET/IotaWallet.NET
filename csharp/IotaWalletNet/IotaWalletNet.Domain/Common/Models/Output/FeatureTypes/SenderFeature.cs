using IotaWalletNet.Domain.Common.Interfaces;

namespace IotaWalletNet.Domain.Common.Models.Output.FeatureTypes
{
    public class SenderFeature : IFeatureType
    {
        public SenderFeature(IAddressType addressType)
        {
            Address = addressType;
        }

        public int Type { get; } = 0;


        public IAddressType Address { get; set; }
    }
}
