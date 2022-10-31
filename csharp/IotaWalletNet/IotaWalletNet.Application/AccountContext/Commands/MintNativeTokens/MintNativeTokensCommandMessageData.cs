using IotaWalletNet.Domain.Common.Models.Coin;
using IotaWalletNet.Domain.Common.Models.Transaction;

namespace IotaWalletNet.Application.AccountContext.Commands.MintNativeTokens
{
    public class MintNativeTokensCommandMessageData
    {
        public MintNativeTokensCommandMessageData(NativeTokenOptions nativeTokenOptions, TransactionOptions options)
        {
            NativeTokenOptions = nativeTokenOptions;
            Options = options;
        }

        public NativeTokenOptions NativeTokenOptions { get; set; }

        public TransactionOptions Options { get; set; }
    }

}
