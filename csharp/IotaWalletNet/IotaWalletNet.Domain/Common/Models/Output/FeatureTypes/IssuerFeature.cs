using IotaWalletNet.Domain.Common.Interfaces;

namespace IotaWalletNet.Domain.Common.Models.Output.FeatureTypes
{
    public class IssuerFeature : IFeatureType
    {
        public IssuerFeature(IAddressType addressType)
        {
            Address = addressType;
        }

        public int Type { get; } = 1;


        public IAddressType Address { get; set; }
    }
}
