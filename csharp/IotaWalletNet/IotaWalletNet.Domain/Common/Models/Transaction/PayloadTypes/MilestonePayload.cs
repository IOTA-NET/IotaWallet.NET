using IotaWalletNet.Domain.Common.Interfaces;
using IotaWalletNet.Domain.Common.Models.Crypto.SignatureTypes;

namespace IotaWalletNet.Domain.Common.Models.Transaction.PayloadTypes
{
    public class MilestonePayload : IPayloadType
    {
        public MilestonePayload(string previousMilestoneId, List<string> parents, string inclusionMerkleRoot, string appliedMerkleRoot, List<Ed25519Signature> signatures)
        {
            PreviousMilestoneId = previousMilestoneId;
            Parents = parents;
            InclusionMerkleRoot = inclusionMerkleRoot;
            AppliedMerkleRoot = appliedMerkleRoot;
            Signatures = signatures;
        }

        public int Type { get; } = 7;

        /// <summary>
        /// The index name.
        /// </summary>
        public ulong Index { get; set; }

        /// <summary>
        /// The timestamp of the milestone.
        /// </summary>
        public ulong Timestamp { get; set; }

        /// <summary>
        /// The protocol version.
        /// </summary>
        public ulong ProtocolVersion { get; set; }

        /// <summary>
        /// [HexEncoded] The id of the previous milestone.
        /// </summary>
        public string PreviousMilestoneId { get; set; }

        /// <summary>
        /// [HexEncoded] The parents where this milestone attaches to.
        /// </summary>
        public List<string> Parents { get; set; }
        /// <summary>
        /// [HexEncoded] The Merkle tree hash of all blocks confirmed by this milestone.
        /// </summary>
        public string InclusionMerkleRoot { get; set; }

        /// <summary>
        /// [HexEncoded] The Merkle tree hash of all blocks applied by this milestone.
        /// </summary>
        public string AppliedMerkleRoot { get; set; }

        /// <summary>
        /// [HexEncoded] The metadata.
        /// </summary>
        public string? Metadata { get; set; }


        public List<IMilestoneOptionType>? Options { get; set; }

        /// <summary>
        /// The signatures.
        /// </summary>
        public List<Ed25519Signature> Signatures { get; set; }
    }
}
