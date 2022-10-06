using IotaWalletNet.Domain.Common.Models.Nft;
using IotaWalletNet.Domain.Common.Models.Transaction;

namespace IotaWalletNet.Application.AccountContext.Commands.MintNfts
{
    public class MintNftsCommandMessageData
    {
        public MintNftsCommandMessageData(List<NftOptions> nftsOptions, TransactionOptions transactionOptions)
        {
            NftsOptions = nftsOptions;
            Options = transactionOptions;
        }
        public List<NftOptions> NftsOptions { get; set; }
        public TransactionOptions Options { get; set; }
    }
}
