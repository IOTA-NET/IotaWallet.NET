using IotaWalletNet.Domain.Common.Models.Transaction;

namespace IotaWalletNet.Application.AccountContext.Commands.BurnNft
{
    public class BurnNftCommandMessageData
    {
        public BurnNftCommandMessageData(string nftId, TransactionOptions options)
        {
            NftId = nftId;
            Options = options;
        }

        public string NftId { get; set; }

        public TransactionOptions Options { get; set; }
    }
}
