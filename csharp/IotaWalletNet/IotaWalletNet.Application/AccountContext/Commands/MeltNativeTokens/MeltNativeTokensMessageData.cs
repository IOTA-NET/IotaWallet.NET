using IotaWalletNet.Domain.Common.Models.Transaction;

namespace IotaWalletNet.Application.AccountContext.Commands.MeltNativeTokens
{
    public class MeltNativeTokensMessageData
    {
        public MeltNativeTokensMessageData(string tokenId, string meltAmount, TransactionOptions options)
        {
            TokenId = tokenId;
            MeltAmount = meltAmount;
            Options = options;
        }

        /// <summary>
        /// The native token id.
        /// </summary>
        public string TokenId { get; set; }

        /// <summary>
        /// [HexEncodedAmount] To be melted amount.
        /// </summary>
        public string MeltAmount { get; set; }

        /// <summary>
        /// The options to define a `RemainderValueStrategy` or custom inputs.
        /// </summary>
        public TransactionOptions Options { get; set; }
    }
}
