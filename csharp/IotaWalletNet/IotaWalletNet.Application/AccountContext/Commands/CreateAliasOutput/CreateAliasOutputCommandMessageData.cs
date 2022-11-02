using IotaWalletNet.Domain.Common.Models.Output;
using IotaWalletNet.Domain.Common.Models.Transaction;

namespace IotaWalletNet.Application.AccountContext.Commands.CreateAliasOutput
{
    public class CreateAliasOutputCommandMessageData
    {
        public CreateAliasOutputCommandMessageData(AliasOutputOptions aliasOutputOptions, TransactionOptions options)
        {
            AliasOutputOptions = aliasOutputOptions;
            Options = options;
        }

        public AliasOutputOptions AliasOutputOptions { get; set; }

        public TransactionOptions Options { get; set; }
    }
}
