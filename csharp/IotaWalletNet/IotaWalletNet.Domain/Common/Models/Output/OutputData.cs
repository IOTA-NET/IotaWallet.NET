namespace IotaWalletNet.Domain.Common.Models.Output
{
    /// <summary>
    /// An output with Metadata
    /// </summary>
    public class OutputData
    {
        public OutputData(string outputId)
        {
            OutputId = outputId;
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

        //addresstypes not mapped
        //outputtypes not mapped

        /// <summary>
        /// The metadata of the output
        /// </summary>
        public OutputMetadata Metadata { get; set; }
    }
}
