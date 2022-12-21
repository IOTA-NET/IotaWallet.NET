using IotaWalletNet.Domain.Common.Extensions;
using IotaWalletNet.Domain.Common.Interfaces;

namespace IotaWalletNet.Domain.Common.Models.Transaction.PayloadTypes
{
    public class TaggedDataPayload : IPayloadType
    {
        public TaggedDataPayload(string tag, string data)
        {
            Tag = tag.ToHexString();
            Data = data.ToHexString();
        }

        public int Type { get; } = 5;

        /// <summary>
        /// [HexEncoded] The tag to use to categorize the data.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// [HexEncoded] The index data
        /// </summary>
        public string Data { get; set; }

    }
}
