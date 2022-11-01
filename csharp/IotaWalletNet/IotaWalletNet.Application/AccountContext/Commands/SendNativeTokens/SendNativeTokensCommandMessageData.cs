using IotaWalletNet.Domain.Common.Models.Address;
using IotaWalletNet.Domain.Common.Models.Transaction;

namespace IotaWalletNet.Application.AccountContext.Commands.SendNativeTokens
{
    public class SendNativeTokensCommandMessageData
    {
        public SendNativeTokensCommandMessageData(List<AddressWithNativeTokens> addressesNativeTokens, TransactionOptions options)
        {
            this.addressesNativeTokens = addressesNativeTokens;
            Options = options;
        }

        public List<AddressWithNativeTokens> addressesNativeTokens { get; set; }

        public TransactionOptions Options { get; set; }
    }
}
