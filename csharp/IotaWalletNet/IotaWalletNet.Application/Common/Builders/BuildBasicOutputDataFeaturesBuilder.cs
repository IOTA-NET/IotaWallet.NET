using IotaWalletNet.Domain.Common.Extensions;
using IotaWalletNet.Domain.Common.Models.Output.FeatureTypes;
using IotaWalletNet.Domain.Common.Models.Output.OutputDataTypes;

namespace IotaWalletNet.Application.Common.Builders
{
    public class BuildBasicOutputDataFeaturesBuilder
    {
        private readonly BuildBasicOutputBuilder _buildBasicOutputBuilder;
        private readonly BuildBasicOutputData _outputData;

        public BuildBasicOutputDataFeaturesBuilder(BuildBasicOutputBuilder buildBasicOutputBuilder, BuildBasicOutputData outputData)
        {
            _buildBasicOutputBuilder = buildBasicOutputBuilder;
            _outputData = outputData;
        }

        public BuildBasicOutputDataFeaturesBuilder AddMetadataFeature(string data)
        {
            MetadataFeature metadataFeature = new MetadataFeature(data.ToHexString());
            _outputData.Features.Add(metadataFeature);

            return this;
        }

        public BuildBasicOutputBuilder Then() => _buildBasicOutputBuilder;
    }
}
