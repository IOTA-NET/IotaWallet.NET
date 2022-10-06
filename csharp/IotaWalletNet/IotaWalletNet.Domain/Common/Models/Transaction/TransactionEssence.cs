using IotaWalletNet.Domain.Common.Models.Output;

namespace IotaWalletNet.Domain.Common.Models.Transaction
{
    public class TransactionEssence
    {
        /// <summary>
        /// The network id of the block
        /// </summary>
        public string NetworkId { get; set; }

        /// <summary>
        /// The inputs of the transaction
        /// </summary>
        public List<UtxoInput> Inputs { get; set; } = new List<UtxoInput>();

        /// <summary>
        /// The commitment to the referenced inputs.
        /// </summary>
        public string InputsCommitment { get; set; }


        //TODO: outputtypes

        /// <summary>
        /// Tagged data payload.
        /// </summary>
        public TaggedDataPayload Payload { get; set; }
    }
}
