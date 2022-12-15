using IotaWalletNet.Domain.Common.Interfaces;
using IotaWalletNet.Domain.Common.Models.Explorer;

namespace IotaWalletNet.Domain.Common.Models.Output
{
    /// <summary>
    /// An output with Metadata
    /// </summary>
    public class OutputData
    {
        public OutputData(
            string outputId,
            string networkId,
            IAddressType address,
            IOutputType outputType,
            OutputMetadataResponse outputMetadata
            )
        {
            OutputId = outputId;
            NetworkId = networkId;
            Address = address;
            Output = outputType;
            Metadata = outputMetadata;
        }

        /// <summary>
        /// The identifier of an Output
        /// </summary>
        public string OutputId { get; set; }

        /// <summary>
        /// If an output is spent
        /// </summary>
        public bool IsSpent { get; set; }

        public string NetworkId { get; set; }

        public bool Remainder { get; set; }

        /// <summary>
        /// Associated account address
        /// </summary>
        public IAddressType Address { get; set; }

        /// <summary>
        /// The actual Output
        /// </summary>
        public IOutputType Output { get; set; }

        /// <summary>
        /// The metadata of the output
        /// </summary>
        public OutputMetadataResponse Metadata { get; set; }
    }
}
