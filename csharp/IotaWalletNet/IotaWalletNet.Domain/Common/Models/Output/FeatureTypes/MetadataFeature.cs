using IotaWalletNet.Domain.Common.Interfaces;

namespace IotaWalletNet.Domain.Common.Models.Output.FeatureTypes
{
    public class MetadataFeature : IFeatureType
    {

        public MetadataFeature(string data)
        {
            Data = data;
        }

        public int Type { get; } = 2;


        /// <summary>
        /// [HexEncoded] Defines metadata (arbitrary binary data) that will be stored in the output.
        /// </summary>
        public string Data { get; set; }
    }
}
