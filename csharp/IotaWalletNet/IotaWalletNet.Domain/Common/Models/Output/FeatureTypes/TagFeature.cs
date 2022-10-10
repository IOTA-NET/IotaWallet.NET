using IotaWalletNet.Domain.Common.Interfaces;

namespace IotaWalletNet.Domain.Common.Models.Output.FeatureTypes
{
    public class TagFeature : IFeatureType
    {
        public TagFeature(string tag)
        {
            Tag = tag;
        }

        public int Type { get; } = 3;


        /// <summary>
        /// [HexEncoded] Defines a tag for the data.
        /// </summary>
        public string Tag { get; set; }
    }
}
