using IotaWalletNet.Domain.Common.Interfaces;
using IotaWalletNet.Domain.Common.Models.Output.InputTypes;

namespace IotaWalletNet.Domain.Common.Models.Transaction.EssenceTypes
{
    public class TransactionEssence : IEssenceType
    {
        public TransactionEssence(int type, string networkId, List<UtxoInput> inputs, string inputsCommitment, List<IOutputType> outputs, TaggedDataPayload payload)
        {
            Type = type;
            NetworkId = networkId;
            Inputs = inputs;
            InputsCommitment = inputsCommitment;
            Outputs = outputs;
            Payload = payload;
        }

        public int Type { get; } = 1;

        /// <summary>
        /// The network id of the block.
        /// </summary>
        public string NetworkId { get; set; }

        /// <summary>
        /// The inputs of the transaction.
        /// </summary>
        public List<UtxoInput> Inputs { get; set; }

        /// <summary>
        /// The commitment to the referenced inputs.
        /// </summary>
        public string InputsCommitment { get; set; }

        /// <summary>
        /// The outputs of the transaction.
        /// </summary>
        public List<IOutputType> Outputs { get; set; }

        /// <summary>
        /// Tagged data payload.
        /// </summary>
        public TaggedDataPayload Payload { get; set; }
    }
}
