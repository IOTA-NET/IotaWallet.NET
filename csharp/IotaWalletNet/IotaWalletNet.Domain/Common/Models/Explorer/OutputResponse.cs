using IotaWalletNet.Domain.Common.Interfaces;

namespace IotaWalletNet.Domain.Common.Models.Explorer
{
    public class OutputResponse
    {
        public OutputResponse(IOutputType output, OutputMetadataResponse metadata)
        {
            Output = output;
            Metadata = metadata;
        }

        public IOutputType Output { get; set; }

        public OutputMetadataResponse Metadata { get; set; }
    }
}
