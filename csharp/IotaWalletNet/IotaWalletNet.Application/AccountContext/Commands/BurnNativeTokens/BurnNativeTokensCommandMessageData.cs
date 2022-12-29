using IotaWalletNet.Domain.Common.Models.Transaction;

namespace IotaWalletNet.Application.AccountContext.Commands.BurnNativeTokens
{
    public class BurnNativeTokensCommandMessageData
    {
        public BurnNativeTokensCommandMessageData(string tokenId, string burnAmount, TransactionOptions options)
        {
            TokenId = tokenId;
            BurnAmount = burnAmount;
            Options = options;
        }

        /// <summary>
        /// The native token id.
        /// </summary>
        public string TokenId { get; set; }

        /// <summary>
        /// [HexEncodedAmount] To be burned amount.
        /// </summary>
        public string BurnAmount { get; set; }

        /// <summary>
        /// The options to define a `RemainderValueStrategy` or custom inputs.
        /// </summary>
        public TransactionOptions Options { get; set; }
    }
}
